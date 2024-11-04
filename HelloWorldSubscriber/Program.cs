using System;
using OpenDDSharp;
using OpenDDSharp.DDS;
using OpenDDSharp.OpenDDS.DCPS;
using Messenger;
using System.Collections.Generic;

namespace HelloWorldSubscriber
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

            Subscriber subscriber = participant.CreateSubscriber();
            if (subscriber == null)
            {
                throw new Exception("Could not create the subscriber");
            }
            
            DataReader reader = subscriber.CreateDataReader(topic);
            if (reader == null)
            {
                throw new Exception("Could not create the message data reader");
            }
            MessageDataReader messageReader = new MessageDataReader(reader);

            while (true)
            {
                StatusMask mask = messageReader.StatusChanges;
                if ((mask & StatusKind.DataAvailableStatus) != 0)
                {
                    List<Message> receivedData = new List<Message>();
                    List<SampleInfo> receivedInfo = new List<SampleInfo>();
                    result = messageReader.Take(receivedData, receivedInfo);

                    if (result == ReturnCode.Ok)
                    {
                        bool messageReceived = false;
                        for (int i = 0; i < receivedData.Count; i++)
                        {
                            if (receivedInfo[i].ValidData)
                            {
                                Console.WriteLine(receivedData[i].subject_id);
                                Console.WriteLine(receivedData[i].from);
                                Console.WriteLine(receivedData[i].subject);
                                Console.WriteLine(receivedData[i].text);
                                Console.WriteLine(receivedData[i].count);
                                messageReceived = true;
                            }
                        }

                        if (messageReceived)
                            break;
                    }
                }

                System.Threading.Thread.Sleep(100);
            }


            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();

            participant.DeleteContainedEntities();
            dpf.DeleteParticipant(participant);
            ParticipantService.Instance.Shutdown();

            Ace.Fini();
        }
    }
}
