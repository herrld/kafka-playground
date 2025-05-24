using Confluent.Kafka;

namespace KafkaWorker01
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer<string, string> consumer;
        private readonly IProducer<string, string> producer;

        public Worker(ILogger<Worker> logger, IConsumer<string, string> consumer)
        {
            _logger = logger;
            this.consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                this.consumer.Subscribe("test-topic");


                while (!stoppingToken.IsCancellationRequested)
                {
                    var item = this.consumer.Consume(stoppingToken);
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        _logger.LogInformation($"from kafka: {item.Value}");

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
