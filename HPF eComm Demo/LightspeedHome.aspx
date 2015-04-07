<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LightspeedHome.aspx.cs" Inherits="HPF_eComm_Demo.LightSpeedHome" %>

<!DOCTYPE html>
<html>
<head>
<title>Lightspeed Payment Page Demo</title>
<!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
<link rel="stylesheet" type="text/css" href="./css/914.css" />
    <style>
        #hpfIFrame
            {
                height: 1000px;
                width: 520px;
            }
    </style>
</head>
<body>
    <div id="wrapper">
        <div id="headerwrap">
        <div id="header">
            <img src="http://i.imgur.com/8V3ttGp.png"
        </div style="height: 97px; width: 384px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>

        <!--
        <div id="navigationwrap">
        <div id="navigation">
            <p>This is the Menu</p>
        </div>
        </div>
        <div id="leftcolumnwrap">
        <div id="leftcolumn">
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
        </div>
        </div>*/
        <div id="contentwrap">
            -->
        <div id="content">
             <iframe id="hpfIFrame" runat="server" scrolling="yes" aria-live="assertive" aria-readonly="False" style="border-style: hidden"></iframe>
        </div>
        </div>
    <!--</div>-->
</body>
</html>

