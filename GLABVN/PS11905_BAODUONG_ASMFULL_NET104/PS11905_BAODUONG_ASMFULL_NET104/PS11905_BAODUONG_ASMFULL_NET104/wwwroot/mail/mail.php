<?
$name = $_POST{'name'};
$email = $_POST{'email'};
$subject = $_POST{'subject'};
$message = $_POST['message'];

$email_message = "

Name: ".$name."
Email: ".$email."
Subject: ".$subject."
Message: ".$message."

";

mail ("duongdtbps11905fpt.edu.vn" , "New Message", $email_message);
header("location: ../mail-send.html");
?>


