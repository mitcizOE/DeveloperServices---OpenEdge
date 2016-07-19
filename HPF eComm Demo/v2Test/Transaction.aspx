<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="HPF_eComm_Demo.Transaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head id="Head1" runat="server">
		<title></title>
		<link href="defaultStylesheet.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            #newFrame
            {
                width: 100%;
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
            .auto-style6 {
                width: 506px;
            }
            .auto-style7 {
                height: 23px;
                width: 148px;
            }
            .auto-style11 {
                height: 23px;
                width: 147px;
            }
            .auto-style12 {
                height: 34px;
            }
            .auto-style14 {
                width: 2px;
            }
            .auto-style16 {
                width: 130px;
            }
            .auto-style17 {
                width: 130px;
                height: 26px;
            }
            .auto-style18 {
                height: 26px;
            }
            </style>
	</head>
	<body>
		<form id="demoForm" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" EnableViewState="False" BackColor="#6DB9E6" BorderStyle="None">
                <div style="font-family: 'Comic Sans MS'; font-size: large; font-weight: bold">Hosted Payment Form ASP.NET Test Page</div>
                <table style="width:100%;">
                    <tr>
                        <td class="auto-style11">Standard Transactions</td>
                        <td class="auto-style7"></td>
                        <td class="auto-style14"></td>
                        <td class="auto-style1">EMV Transactions</td>
                        <td>(RCM and Hardware required)</td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:Button ID="creditSaleTransaction" runat="server" OnClick="creditSaleTransaction_Click" Text="Credit Card Sale" Width="139px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="28px" />
                        </td>
                        <td class="auto-style4">
                            <asp:Button ID="creditReturnTransaction" runat="server" OnClick="creditReturnTransaction1_Click" Text="Credit Card Return" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
                        </td>
                        <td class="auto-style14"></td>
                        <td class="auto-style3">
                            <asp:Button ID="emvCreditSaleTransaction" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="28px" Text="EMV Credit Sale" Width="139px" OnClick="emvCreditSaleTransaction_Click" />
                        </td>
                        <td>
                            <asp:Button ID="emvCreditReturnTransaction" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="28px" Text="EMV Credit Return" Width="139px" OnClick="emvCreditReturnTransaction_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Button ID="checkSaleTransaction" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="28px" Text="Check Sale" Width="139px" OnClick="checkSaleTransaction_Click" />
                        </td>
                        <td class="auto-style2">
                            <asp:Button ID="checkCreditTransaction" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="28px" Text="Check Credit" Width="139px" OnClick="checkCreditTransaction_Click" />
                        </td>
                        <td class="auto-style14"></td>
                        <td class="auto-style1">
                            <asp:Button ID="debitSaleTransaction" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" OnClick="debitSaleTransaction_Click" Text="Debit Sale" Width="140px" />
                        </td>
                        <td>
                            <asp:Button ID="debitReturnTransaction" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" OnClick="debitReturnTransaction_Click" Text="Debit Return" Width="140px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:Button ID="createAliasTransaction" runat="server" OnClick="createAliasTransaction_Click" Text="Create Alias" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" />
                        </td>
                        <td class="auto-style4">
                            <asp:Button ID="checkAliasCreateTransaction" runat="server" OnClick="checkAliasCreateTransaction_Click" Text="Create Check Alias" Width="140px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="30px" CausesValidation="False" />
                        </td>
                        <td class="auto-style14"></td>
                        <td class="auto-style3"></td>
                        <td class="auto-style12"></td>
                    </tr>
                </table>
            </asp:Panel>
                            
                            <asp:Button ID="AdvancedButton" runat="server" OnClick="CustomCredsButton_Click" Text="Advanced" Width="139px" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="23px" />
                                    
                               
            
                        <asp:Panel ID="CustomCredsPanel" runat="server" ScrollBars="Vertical" BackColor="LightGray" Visible="False">
                            <br />
                            <asp:CheckBox ID="boxCustomCreds" runat="server" Text="Use Custom XWeb Credentials" />
                            <br />
                            <table style="width:100%;">
                                <tr>
                                    <td class="auto-style17">XWeb ID</td>
                                    <td class="auto-style18">
                                        <asp:TextBox ID="customXWebID" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style18"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Authentication Key</td>
                                    <td>
                                        <asp:TextBox ID="customAuthKey" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">Terminal ID</td>
                                    <td class="auto-style18">
                                        <asp:TextBox ID="customTerminalID" runat="server" Width="199px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style18"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style16">Industry</td>
                                    <td>
                                        <asp:TextBox ID="customIndustry" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <asp:Button ID="saveButton" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="23px" OnClick="saveButton_Click" Text="Save" Width="139px" />
                            <br />
                            <br />
                            <asp:Button ID="HideButton" runat="server" BackColor="DarkGray" BorderStyle="None" EnableViewState="False" Font-Bold="True" ForeColor="White" Height="23px" OnClick="HideButton_Click" Text="Hide" Width="139px" />
            </asp:Panel>
            <br />
            </div>
			<asp:scriptmanager ID="defaultScriptManager" runat="server"></asp:scriptmanager>
			<div>
				<table dir="ltr" style="width:100%;">
                    <tr>
                        <td class="auto-style6" style="width: 37%">

                            <iframe id="newFrame" runat="server" style="border-style: hidden; height: 575px;"></iframe>
			            </td>
                        <td>
                            <asp:Panel ID="XMLPanel" runat="server">
                                OtK Call XML:<br />
                                <asp:TextBox ID="otkCall" runat="server" Height="257px" TextMode="MultiLine" Width="412px"></asp:TextBox>
                                <br />
                                <br />
                                OtK Call Result:<br />
                                <asp:TextBox ID="resultsXML" runat="server" Height="263px" TextMode="MultiLine" Width="413px"></asp:TextBox>
                            </asp:Panel>
                        </td>                  
                    </tr>
                </table>
                <br />
			</div>
            <div style="height: 163px">
            <asp:Timer ID="TimerResultCall" OnTick="TimerResultCall_Tick" runat="server" 
                    Interval="10000"></asp:Timer>
            <asp:UpdatePanel ID="UpdatePanelResult" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    Results Call:<br />
                    <asp:TextBox ID="TextBoxResultDisplay" runat="server" Height="116px" 
                        ReadOnly="True" Width="191px" TextMode="MultiLine" Font-Size="Large" OnDataBinding="TextBoxResultDisplay_DataBinding" OnTextChanged="TextBoxResultDisplay_TextChanged"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TimerResultCall" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
            </div>			
		</form>
	</body>
</html>
