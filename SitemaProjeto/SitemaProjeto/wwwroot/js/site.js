// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#myTable').DataTable({
        pagingType: "numbers",
        lengthChange: false,
        pageLength: 5,
        infoCallback: function (settings, start, end, max, total, pre) {
            return '';
        },
        language: {
            search: "Pesquisa",
            info: ""
        }
    });
});

$(document).ready(function () {
    $('#myTableProject').DataTable({
        pagingType: "numbers",
        lengthChange: false,
        pageLength: 5,
        infoCallback: function (settings, start, end, max, total, pre) {
            return '';
        },
        language: {
            search: "Pesquisa",
            info: ""
        },
        columnDefs: [
            { targets: [2], searchable: true }
        ]
    });
});