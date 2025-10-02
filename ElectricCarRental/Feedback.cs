namespace ElectricCarRental;

// Прості відгуки/звернення клієнтів замість SupportTicket
public class Feedback
{
	public string FeedbackId { get; set; }
	public string Message { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
} 