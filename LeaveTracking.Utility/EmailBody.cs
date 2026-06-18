using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Utility
{
	public static class EmailBody
	{
		public static string EmailstringBody(string email, string emailtoken) 
		{
			return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Reset Password</title>
</head>
<body style='margin:0;padding:0;font-family:Arial,Helvetica,sans-serif;background:#f4f4f4'>
    <div style='max-width:600px;margin:40px auto;background:white;border-radius:8px;box-shadow:0 0 10px rgba(0,0,0,0.1);overflow:hidden'>
        <div style='padding:30px;text-align:center;background:linear-gradient(to right, #6e6ef6, #a3a3ff);color:white'>
            <h2 style='margin:0'>Reset Your Password</h2>
        </div>
        <div style='padding:30px'>
            <p>Hello,</p>
            <p>You're receiving this email because you requested a password reset for your account.</p>
            <p>Please click the button below to choose a new password:</p>
            <div style='text-align:center;margin:30px 0'>
                <a href='http://localhost:4200/resetpassword?email={email}&code={emailtoken}' 
                   style='background:#0d6efd;color:white;padding:12px 24px;text-decoration:none;
                          border-radius:5px;display:inline-block;font-weight:bold'>
                    Reset Password
                </a>
            </div>
            <p>If you didn’t request this, you can safely ignore this email.</p>
            <p>Regards,<br><strong>Leave Tracking App Team</strong></p>
        </div>
    </div>
</body>
</html>";
		}
	}
}
