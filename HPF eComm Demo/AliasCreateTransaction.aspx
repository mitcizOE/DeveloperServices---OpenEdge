<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AliasCreateTransaction.aspx.cs" Inherits="HPF_eComm_Demo.CheckAliasCreateTransaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head id="Head1" runat="server">
		<title></title>
		<link href="defaultStylesheet.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            #xwebIFrame
            {
                width: 100%;
                height: 269px;
            }
        </style>
	</head>
	<body>
		<form id="demoForm" runat="server">
			<asp:scriptmanager ID="defaultScriptManager" runat="server"></asp:scriptmanager>
			<div>
				<iframe id="xwebIFrame" runat="server" scrolling="yes" style="border-style: hidden"></iframe>
			</div>
            <div style="height: 163px">
            <asp:Timer ID="TimerResultCall" OnTick="TimerResultCall_Tick" runat="server" 
                    Interval="5000"></asp:Timer>
            <asp:UpdatePanel ID="UpdatePanelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <asp:TextBox ID="TextBoxResultDisplay" runat="server" Height="116px" 
                        ReadOnly="True" Width="191px" TextMode="MultiLine" Font-Size="Large" Visible="False" ViewStateMode="Enabled"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TimerResultCall" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
            </div>			
		</form>
	</body>
</html>
