//bağlantıyı oluşturma (rabbitMQ sunucusuna bağlantı)
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://dofvzijr:Pp-ijBOk2uCxP4za2Fys0bWsVcz8CmCu@vulture.rmq.cloudamqp.com/dofvzijr");

//bağlantı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma (messajın okunacağı kuyruğu oluşturma)
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//Queue'den mesaj okuma (kuyruktaki mesajları okuma)
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
    //kuyruğa gelen mesajın işlenmesi
    //e.Body(): kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray(): kuyruktaki mesajın byte verisine getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();