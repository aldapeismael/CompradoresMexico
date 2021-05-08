

$(document).ready(function () {
    //Home Js´s

    $("#menu").click(function () {
        $("#menuUser").toggle();
    });

    $("#menuSerch").click(function () {
        $("#menuSerchAd").toggle();
    });

    $("#menuSerchClose").click(function () {
        $("#menuSerchAd").toggle();
    });
    
// LOGIN Js´s
    $("#menuProfile").click(function () {
        $("#menuUser").toggle();
    });

// addProduct

    $("#menuAddProduct").click(function(){
        $("#menuProductModal").toggle();
    })

    $("#AddProductClose").click(function(){
        $("#menuProductModal").toggle();
    })

});

