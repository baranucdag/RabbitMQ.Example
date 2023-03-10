using RabbitMQ.Client;
using System.Text;

//bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://dofvzijr:Pp-ijBOk2uCxP4za2Fys0bWsVcz8CmCu@vulture.rmq.cloudamqp.com/dofvzijr");

//bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// kuyruk oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue'ya mesaj gönderme (kuyruğa atacağı mesajları byte türünden kabul eder)
//byte[] message =  Encoding.UTF8.GetBytes("Test");
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Test " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.Read();
