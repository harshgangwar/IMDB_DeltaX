
$(document).ready(function () {
    loadItems();
});  

function loadItems() {   
    $.ajax({  
        url: getMoviesData,
        type: "GET",  
        dataType: "json",  
        headers: {  
            "accept": "application/json;odata=verbose"  
        },  
        success: mySuccHandler,  
        error: myErrHandler  
    });  

    $("#tblMovieGrid").on("click", ".btnEditMovie", function () {
        updateMovieId = $(this).data('id');
        $.ajax({
            url: getMoviesDetailsById,
            headers: addAntiForgery(),
            data: { pkMovieId: updateMovieId },
            success: function (data) {
                if (data != null) {
                    dataBindingToModal(data);
                } else {
                    alert("Something wrong, can't edit row");
                }
            },
            error: function () {
                alert("Something wrong, can't edit row");
            }
        });
    });

    function dataBindingToModal(data) {
        $("#moviePkId").val(data.Id);
        $("#plotEditId").val(data.Plot);
        $('#movieName').val(data.Name);
    }

    // Adding verification token at header
    function addAntiForgery() {
        var form = $("#createDataTypeForm");
        var headers = { "__RequestVerificationToken": $('input[name="__RequestVerificationToken"]', form).val() };
        return headers;
    }

} 

function mySuccHandler(data) {

    //try {
    //    var dataTableExample = $('#tblMovieGrid').DataTable();
    //    if (dataTableExample != 'undefined') {
    //        dataTableExample.destroy();
    //    }
    //    dataTableExample = $('#tblMovieGrid').DataTable({
    //        "processing": true,
    //        "serverSide": true,
    //        "filter": false,
    //        scrollY: 300,
    //        "aaData": data,
    //        "columnDefs":
    //        [{
    //                "targets": [0],
    //                "visible": false,
    //                "searchable": false
    //            }], 
    //        "aoColumns": [
    //            { "data": "Id", "name": "Id", "autoWidth": true },
    //            { "data": "Name", "name": "Name", "autoWidth": true },
    //            { "data": "YearofRelease", "name": "YearofRelease", "autoWidth": true },
    //            { "data": "Plot", "title": "ContactName", "name": "Plot", "autoWidth": true },
    //            { "data": "ProducerName", "name": "ProducerName", "autoWidth": true },
    //            //{ "data": "ActorName", "name": "ActorName", "autoWidth": true }
    //            ]

    //    });
    //} catch (e) {
    //    alert(e.message);
    //}
}

function myErrHandler(data, errCode, errMessage) {
    alert("Error: " + errMessage);
} 