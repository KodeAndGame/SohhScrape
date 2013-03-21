<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="SohhScrape.about" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>About</title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"> 
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=no;"> 
    <link rel="icon" type="image/vnd.microsoft.icon" href="images/favicon.ico">
    <link rel="apple-touch-icon" href="images/apple-touch-icon.png">
    <style type="text/css">
        @import "images/styleMainV28.css";
        body
        {
            font-size: 100%;
        }
    </style>
</head>
<body class="pinstripes">
    <div id="bodyAbout">
        <a name="top"></a>
        <div id="headerToolbar" class="toolbar">
            <h1>
                About</h1>
            <a class="backButton" href="./index.aspx"><span>Main</span></a>
        </div>
        <div id="about">
            <h3>MPiff.com</h3>
            <p>
                This web application is independently developed and maintained by a SOHH.com user. 
                It is in no way affiliated, sanctioned or authorized by SOHH.com or its owners
                and designees. 
            </p>

            <p>
                MPiff.com was inspired by <a href="http://secondapps.com/neogaf/">NeoGAF Mobile</a>. 
                Although most code was developed independently, much of the UI and functionality was 
                implemented to closely match that of NeoGAF Mobile. This allows easier browsing of 
                the SOHH forums when using a mobile device. 
            </p>
                
            <p>
                This project is very much a "Work In Progress" and as such, a lot of functionality 
                is still missing or not 100% working. To that end, please be patient as I build
                a fuller solution and determine a way for users to provide feedback.
            </p>

            <h3>Version 0.30</h3>
            <p>
            The current version (0.30) of MPiff.com includes the following features:
            <ul>
                <li>Browsing Forums and Threads</li>
                <li>Logging In</li>
                <li>Posting Replies</li>
                <li>Editing Posts</li>
                <li>Posting New Threads</li>
            </ul>
            </p>
            
            <h3>Donations</h3>
            <p>
            Several visitors have asked about providing donations. Once again, I'd like to reiterate 
            that MPiff.com is in no way affiliated with SOHH.com. Any donations made do not go to 
            SOHH.com but instead to the wholly independent effort of MPiff.com. Additionally, 
            donations will not be used for personal gain. They will instead go towards supporting SOHH 
            Mobile. Primarily, donations will be used towards hosting costs.
            </p>

            <form action="https://www.paypal.com/cgi-bin/webscr" method="post">
            <input type="hidden" name="cmd" value="_s-xclick">
            <input type="hidden" name="encrypted" value="-----BEGIN PKCS7-----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-----END PKCS7-----
">
            <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif"
                border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
            <img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif"
                width="1" height="1">
            </form>
        </div>
        <div id="footerToolbar" class="toolbar">
            <a class="backButton" href="./"><span>Main</span></a>
        </div>
    </div>
</body>
</html>
