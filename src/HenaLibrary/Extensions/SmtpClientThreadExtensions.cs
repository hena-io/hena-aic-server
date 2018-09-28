using Hena.Threads;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public static class SmtpClientThreadExtensions
	{
		static ThreadWorkerPool MailThreadPool = new ThreadWorkerPool();
		public static async Task SendMailThreadAsync(this SmtpClient client, MailMessage message)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

			MailThreadPool.AddWork(() =>
			{
				client.Send(message);
				taskCompletionSource.TrySetResult(true);
			});
			await taskCompletionSource.Task;
		}
	}
}
