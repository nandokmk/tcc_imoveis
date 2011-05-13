<?php

		$name = trim(stripslashes($_POST['input_name'])); 
		$to = "dunnhillstephen@gmail.com"; //your email
		$email = trim(stripslashes($_POST['input_email']));
		$subject = trim(stripslashes($_POST['input_subject']));
		if ( $subject == '' ) :
			$subject = 'Quick Contact';
		endif;
		$message_raw = $_POST['textarea_message'];
	
		$headers = 	'From: ' . 'Admin ' . "<" . $to . ">\r\n";
		$headers .=	'Reply-To: ' . $to . "\r\n";
		$headers .=	'X-Mailer: PHP/'.phpversion();
		$headers .= 'MIME-Version: 1.0' . "\r\n";
		$headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";
			
		$message =	'<html><body style="font: 12px Arial;">';
		$message .=	'<p><strong>Name</strong>: ' . $name . '</p>';
		$message .=	'<p><strong>Email</strong>: ' . $email . '</p>';
		$message .=	'<p><strong>Subject</strong>: ' . $subject . '</p>';
		$message .=	'<strong>Message</strong>: ' . nl2br($message_raw);
		$message .=	'</body></html>';
	
	  	// send email  
		$success = mail($to, $subject, $message, $headers);
		
		// return results
		if ($success) :
			echo '<h3>Message Sent!</h3>';
			echo '<p>Thank you for taking the time to contact us. We value your opinions and appreciate your concerns. A customer service representative will be contacting you soon.</p>';
		else :
			echo '<br /><h4>Message Sending Failed!</h4>';
		endif;
		
  
?>
