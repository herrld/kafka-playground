namespace KafkaWorker01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            //builder.AddKafkaProducer<string, string>("kafkaTestTopic1");
            builder.AddKafkaConsumer<string, string>("kafkaTestTopic1");
            var host = builder.Build();
            host.Run();
        }
    }
}