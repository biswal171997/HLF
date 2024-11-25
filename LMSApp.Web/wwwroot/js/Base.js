var ajaxCallParams = {};
var ajaxDataParams = {};
// General function for all ajax calls
function ajaxCall(callParams, dataParams, callbacksucess,callbackerror,csrftoken) {   
    $.ajax({
        type: callParams.Type,
        url: callParams.Url,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                csrftoken);
        },
        quietMillis: 100, //Number of milliseconds to wait for the user to stop typing before issuing the ajax request
		content: 'application/json;charset=utf-8',
        dataType:'json',
        data: dataParams,
        cache: true,
        success: function (response){
            callbacksucess(response);
        },
        error: function (response){
            callbackerror(response);
}        
    });
}
//Example
//csrftoken for getting Csrf token for page preventing CSRF attacks
// ajaxCallParams.Type = "POST"; // POST type function 
// ajaxCallParams.Url = "/Payment/Create"; // Pass Complete end point Url e-g Payment Controller, Create Action
// ajaxCallParams.DataType = "JSON"; // Return data type e-g Html, Json etc
    
////Set Data parameters 
// ajaxDataParams.Id = 1;
// ajaxDataParams.Name = "Shujat Munawar";
////Passing call and data parameters to general Ajax function
// ajaxCall(ajaxCallParams, ajaxDataParams, function (result) {
     // console.log(result);
// }); 