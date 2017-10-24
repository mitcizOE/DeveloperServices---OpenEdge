
function PinPadSetup() {
	// Invoke the Pin Pad Device set up screen using XML parameters
	var paramString;
	paramString = '<XLINKEMVREQUEST>' +
					   '<TRANSACTIONTYPE>PPDEDITSETUP</TRANSACTIONTYPE>' +
				   '</XLINKEMVREQUEST>';
	
	SendRequest(paramString);
					   
}

function PromptForSignature() {
	// Invoke a signature prompt using XML parameters
	var paramString;
	paramString = '<XLINKEMVREQUEST>' + 
				  '  <TRANSACTIONTYPE>PPDPROMPTSIGNATURE</TRANSACTIONTYPE>' +
				  '  <TITLE>Please Sign</TITLE>' +
				  '  <DISPLAYCAPTUREDSIGNATURE>T</DISPLAYCAPTUREDSIGNATURE>' +
				  '</XLINKEMVREQUEST>';
	
	SendRequest(paramString);
					   
}

function CreditSale() {
	// Invoke a Credit Sale transaction using XML parameters
	var paramString;
	paramString = '<XLINKEMVREQUEST>' +
			  '  <TRANSACTIONTYPE>CREDITSALE</TRANSACTIONTYPE>' +
			  '  <XWEBAUTHKEY>' + $('#txtXWebAuthKey').val() + '</XWEBAUTHKEY>' +
			  '  <XWEBTERMINALID>' + $('#txtXWebTerminalID').val() + '</XWEBTERMINALID>' +
			  '  <XWEBID>' + $('#txtXWebID').val() + '</XWEBID>' +
			  '  <ALLOWDUPLICATES />' +
			  '  <AMOUNT>2.09</AMOUNT>' +
			  '</XLINKEMVREQUEST>';
	
	SendRequest(paramString);
					   
}


// Make an call to RCM to process the transaction
function SendRequest(paramString) {

	DisplayResponse('');
	
	try {
		
		$.ajax(
		{
			type: "GET",
			url: 'https://localsystem.paygateway.com:21113/RcmService.svc/Initialize',
			data: 'xl2Parameters=' + paramString,
			contentType: "application/json",
			dataType: "jsonp",
			crossDomain: true,
			jsonpCallback: 'jsonpResponse',
			cache: false,
			beforeSend: function (xhr) { xhr.setRequestHeader('Access-Control-Allow-Origin', '*'); },
			statusCode:
				{
					404: function () {
						DisplayResponse('Could not contact server.');
					},
					500: function () {
						DisplayResponse('A server-side error has occurred.');
					}
				},
			success: DisplayResponse,
			error: DisplayResponse
		});

	} 
	catch (err) {
		DisplayResponse('An error occured. Please restart the Remote Client Manager.');
	}

	// Write the results to textarea
	function DisplayResponse(result) {
		if (result == null) {
			$('#txtResults').val('No result received from device.');
			return;
		}

		if (result.RcmResponse == null) {
			$('#txtResults').val(result);
			return;
		}

		$('#txtResults').val(result.RcmResponse);
		
	}
	
}