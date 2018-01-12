$(document).ready(function () {

    $('#btnConsultar').click(function () {

        var model = {
            codCliente: $('#txtCodCliente').val(),
            codun: $('#txtCodun').val(),
            razaoSocial: $('#txtRazaoSocial').val(),
            nomeFantasia: $('#txtNomeFantasia').val(),
            cnpj: $('#txtCnpj').val().replace(/\.|-|[/]/g, ''),
            representante: {
                idRepresentante: $('#optRepresentante').val()
            }
        };
        $.ajax({
            type: "POST",
            url: "/AreaRestrita/Cliente/Consulta",
            data: model,
            success: function (lista) {
                debugger;
            },
            error: function (e) {
                console.log(e.status);
            }
        });
    });
});