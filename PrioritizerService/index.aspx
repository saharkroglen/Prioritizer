<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PrioritizerService.Download" %>
<!DOCTYPE HTML>
<html>
<head>
	<meta charset="UTF-8">
	<title>Priori Download</title>
	<link rel="stylesheet" href="web/style/style.css" type="text/css">
</head>
<body>
	<div id="header">
		<div>
			<div <%--class="logo"--%>>
            <img src="web/Images/header.png" />
				<a href="index.aspx"></a>
			</div>
			<ul id="navigation">
				<li class="active">
					<a href="index.aspx">Home</a>
				</li>
				<li>
					<a href="#">Features</a>
				</li>
				<li>
					<a href="#">News</a>
				</li>
				<li>
					<a href="#">About</a>
				</li>
				<li>
					<a href="#">Contact</a>
				</li>
			</ul>
		</div>
	</div>
	<div id="adbox">
		<div class="clearfix">
			<img src="web/images/box.png" alt="Img" height="342" width="368">
			<div>
				<h1>Priori</h1>
				<h2>Collaborate your meetings Action Items,
                    <br />
                    Prioritize your tasks.</h2>
				<p>
					Orginizing your company meeting action items becomes simple with Priori.<br />One place for all meetings, One platform to collaborate and monitor all action items.
                    <p>
					    <span><a href="clientInstallation/PrioriSetup.msi" class="btn">Try It Now!</a><b>Don’t worry it’s for free</b></span>
				    </p>
				</p>
			</div>
		</div>
	</div>
	<div id="contents">
		<div id="tagline" class="clearfix">
			<h1>Design With Simplicity.</h1>
			<div>
				<p>
					Personal Tray View
				</p>
				<img src="web/Images/UserSnap.png" />
			<br />
				<p>
					Meeting View
				</p>
                <img src="web/Images/MeetingSnap.png" />
				
			</div>
		</div>
	</div>
	<div id="footer">
		<div class="clearfix">
			<div id="connect">
				<a href="#" target="_blank" class="facebook"></a><a href="#" target="_blank" class="googleplus"></a>
                <a href="#" target="_blank" class="twitter"></a><a href=#" target="_blank" class="tumbler"></a>
			</div>
			<p>
				© 2013 Priori. All Rights Reserved.
			</p>
		</div>
	</div>
</body>
</html>