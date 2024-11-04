using System;
using OpenDDSharp;
using OpenDDSharp.DDS;
using OpenDDSharp.OpenDDS.DCPS;
using Messenger;

namespace HelloWorldPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Ace.Init();

            DomainParticipantFactory dpf = ParticipantService.Instance.GetDomainParticipantFactory("-DCPSConfigFile", "rtps.ini");
            DomainParticipant participant = dpf.CreateParticipant(42);
            if (participant == null)
            {
                throw new Exception("Could not create the participant");
            }

            // Include your program here

            MessageTypeSupport support = new MessageTypeSupport();
            ReturnCode result = support.RegisterType(participant, support.GetTypeName());
            if (result != ReturnCode.Ok)
            {
                throw new Exception("Could not register type: " + result.ToString());
            }

            Topic topic = participant.CreateTopic("Movie Discussion List", support.GetTypeName());
            if (topic == null)
            {
                throw new Exception("Could not create the message topic");
            }

            OpenDDSharp.DDS.Publisher publisher = participant.CreatePublisher();
            if (publisher == null)
            {
                throw new Exception("Could not create the publisher");
            }

            DataWriter writer = publisher.CreateDataWriter(topic);
            if (writer == null)
            {
                throw new Exception("Could not create the data writer");
            }
            MessageDataWriter messageWriter = new MessageDataWriter(writer);

            Console.WriteLine("Waiting for a subscriber...");
            PublicationMatchedStatus status = new PublicationMatchedStatus();
            do
            {
                writer.GetPublicationMatchedStatus(ref status);
                System.Threading.Thread.Sleep(500);
            }
            while (status.CurrentCount < 1);

            Console.WriteLine("Subscriber found, writing data....");
            messageWriter.Write(new Message
            {
                subject_id = 99,
                from = "Comic Book Guy",
                subject = "Review",
                text = "Worst. Movie. Ever.",
                count = 0
            });

            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();

            participant.DeleteContainedEntities();
            dpf.DeleteParticipant(participant);
            ParticipantService.Instance.Shutdown();

            Ace.Fini();
        }
    }
}
