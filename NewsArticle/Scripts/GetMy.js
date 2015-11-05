$(function () {
    var URL = "/Article/GetMyArticles";
    var deleteUrl = "/Article/DeleteArticle";
    var editUrl = "/Article/EditArticle";
    $.get(URL, function (data) {
        var i;
        for (i = 0; i < data.length; i++) {
            var title = document.createElement("h3");
            var titleNode = document.createTextNode(data[i].Title);
            title.appendChild(titleNode);
            title.setAttribute("contentEditable", true);
            title.id = "title"+data[i].Id;

            var para = document.createElement("span");
            var node = document.createTextNode(data[i].Description);
            para.appendChild(node);
            para.setAttribute("contentEditable", true);
            para.id = "desc"+data[i].Id;

            var deleteButton = document.createElement("button");
            var deleteNode = document.createTextNode("delete");
            deleteButton.appendChild(deleteNode);
            deleteButton.id = "delete" + data[i].Id;
            deleteButton.setAttribute("class", "deleteButtons");
            deleteButton.addEventListener("click", function (event) {
                var d = event.target.id;
                var did = d.substr(6);
                $.post(deleteUrl, { id: did }, function (data) { alert("deleted"); });
            });

            var editButton = document.createElement("button");
            var editNode = document.createTextNode("edit");
            editButton.appendChild(editNode);
            editButton.id = "edit" + data[i].Id;
            editButton.addEventListener("click", function (event) {
                var d = event.target.id;
                var did = d.substr(4);
                var title = $("#title" + did)[0].innerText;
                var description = $("#desc"+did)[0].innerText;
                $.post(editUrl, { id: did, title:title, description: description}, function (data) { alert("edited"); });
            });

            var breakLine = document.createElement("br");
            var element = document.getElementById("myArticlesId");
            element.appendChild(title);
            element.appendChild(para);
            element.appendChild(breakLine);
            element.appendChild(deleteButton);
            element.appendChild(editButton);
        }

    });
});



