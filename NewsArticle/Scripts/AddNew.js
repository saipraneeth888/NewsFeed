function addNew() {
    var Url = "/Article/AddNew";
    var title = $("#titleId")[0].innerText;
    var description = $("#descriptionId")[0].innerText;

    $.post(Url, { title: title, description: description }, function (data) { alert("added"); });
}