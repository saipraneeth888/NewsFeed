$(function () {
    var URL = "/Article/GetAll" ;
    $.get(URL, function(data) {
        var i;
        for (i = 0; i < data.length; i++) {
            var title = document.createElement("h3");
            var titleNode = document.createTextNode(data[i].Title);
            title.appendChild(titleNode);
            var para = document.createElement("p");
            var node = document.createTextNode(data[i].Description);
            para.appendChild(node);

            var element = document.getElementById("newsFeedId");
            element.appendChild(title);
            element.appendChild(para);
        }

    });

});