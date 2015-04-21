<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="HPF_eComm_Demo.Transaction" %>

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
            .auto-style1 {
                width: 147px;
            }
            .auto-style2 {
                width: 148px;
            }
            .auto-style3 {
                width: 147px;
                height: 34px;
            }
            .auto-style4 {
                width: 148px;
                height: 34px;
            }
            .auto-style5 {
                height: 34px;
            }
            .auto-style6 {
                width: 506px;
            }
        </style>
	</head>
	<body>
		<form id="demoForm" runat="server">
        <div>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style3">
        <asp:Button ID="creditSaleTransaction" runat="server" OnClick="creditSaleTransaction_Click" Text="Credit Card Sale" Width="139px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="29px" />
                    </td>
                    <td class="auto-style4">
        <asp:Button ID="creditReturnTransaction" runat="server" OnClick="creditReturnTransaction1_Click" Text="Credit Card Return" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
                    </td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style1">
        <asp:Button ID="debitSaleTransaction" runat="server" OnClick="debitSaleTransaction_Click" Text="Debit Sale" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
                    </td>
                    <td class="auto-style2">
        <asp:Button ID="debitReturnTransaction" runat="server" OnClick="debitReturnTransaction_Click" Text="Debit Return" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
                    </td>
                    <td>NOT YET IMPLEMENTED!</td>
                </tr>
                <tr>
                    <td class="auto-style1">
        <asp:Button ID="createAliasTransaction" runat="server" OnClick="createAliasTransaction_Click" Text="Create Alias" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
                    </td>
                    <td class="auto-style2">
        <asp:Button ID="checkAliasCreateTransaction" runat="server" OnClick="creditSaleTransaction_Click" Text="Create Check Alias" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" CausesValidation="False" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
            </div>
			<asp:scriptmanager ID="defaultScriptManager" runat="server"></asp:scriptmanager>
			<div>
				<table dir="ltr" style="width:100%;">
                    <tr>
                        <td class="auto-style6">
				<iframe id="xwebIFrame" runat="server" scrolling="yes" style="border-style: hidden"></iframe>
			            </td>
                        <td>OtK Call XML:<br />
            <asp:TextBox ID="otkCall" runat="server" Height="257px" TextMode="MultiLine" Width="412px"></asp:TextBox>
            <br />
            <br />
                            OtK Call Result:<br />
            <asp:TextBox ID="resultsXML" runat="server" Height="263px" TextMode="MultiLine" Width="413px"></asp:TextBox>
                        </td>                  
                    </tr>
                </table>
                <br />
			</div>
            <div style="height: 163px">
            <asp:Timer ID="TimerResultCall" OnTick="TimerResultCall_Tick" runat="server" 
                    Interval="5000"></asp:Timer>
            <asp:UpdatePanel ID="UpdatePanelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <asp:TextBox ID="TextBoxResultDisplay" runat="server" Height="116px" 
                        ReadOnly="True" Width="191px" TextMode="MultiLine" Font-Size="Large" Visible="False"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TimerResultCall" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
            </div>			
		</form>
	</body>
</html>
