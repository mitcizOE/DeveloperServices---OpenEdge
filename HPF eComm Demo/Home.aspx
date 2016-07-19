<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Home.aspx.cs" Inherits="HPF_eComm_Demo.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="defaultStylesheet.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            #hpfIFrame
            {
                height: 455px;
                width: 491px;
            }
            .auto-style2 {
                width: 100%;
                background-color: #D5E8C0;
            }
            .newStyle1 {
                font-family: Calibri;
            }
            


        </style>
</head>
<body>
    <form id="homeForm" runat="server">
    <div>
        <br />
        <table class="auto-style2">
            <tr>
                <td style="font-weight: 700">Step 1 - Select automatic payment type:</td>
            </tr>
        </table>
        <br />
        <asp:Button ID="creditSaleTransaction" runat="server" OnClick="creditSaleTransaction_Click" Text="Credit Card Sale" Width="140px" BackColor="#2874B8" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
        <asp:Button ID="createAliasButton" runat="server" Text="Create Card-On-File" Width="140px" BackColor="#2874B8" BorderStyle="None" Font-Bold="True" ForeColor="White" Height="30px" OnClick="createAliasButton_Click" style="margin-bottom: 0px; margin-left: 8px;" />
        &nbsp;
        <asp:Button ID="checkAliasTransaction" runat="server" Text="Create ACH-On-File" Width="140px" BackColor="#2874B8" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" OnClick="checkAliasTransaction_Click" style="margin-top: 0px" />
        <br />
        <br />
        <table class="auto-style2">
            <tr>
                <td style="font-weight: 700">Step 2 - Complete payment information:</td>
            </tr>
        </table>
        <div>
        <%--<iframe id="hpfIFrame" runat="server" scrolling="yes" aria-live="assertive" aria-readonly="False" style="border-style: hidden"></iframe>--%>
    </div>
    </div>
    </form>
    
</body>
</html>
