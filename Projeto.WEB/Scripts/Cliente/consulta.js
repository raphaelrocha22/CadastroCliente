$(document).ready(function () {

    $('.date').datepicker({
    });

    $('#btnConsultar').click(function () {
        Consultar();
    });
});

function Consultar() {

    var model = {
        CodCliente: $('#txtCodCliente').val(),
        Codun: $('#txtCodun').val(),
        RazaoSocial: $('#txtRazaoSocial').val(),
        NomeFantasia: $('#txtNomeFantasia').val(),
        Cnpj: $('#txtCnpj').val().replace(/\.|-|[/]/g, ''),
        DataInicio: $('#txtDataInicio').val(),
        DataFim: $('#txtDataFim').val() + " 23:59:59",
        IdRepresentante: $('#optRepresentante').val()
    };
    $.ajax({
        type: "POST",
        url: "/AreaRestrita/Cliente/Consulta",
        data: model,
        beforeSend: function () {
            $('#btnConsultar').prop('value', 'Aguarde...').prop('disabled', true);
        },
        success: function (lista) {
            var conteudo = "";

            $.each(lista, function (i, m) {

                conteudo += "<tr>";
                
                conteudo += "<td>" + m.IdCliente + "</td>";
                conteudo += "<td style='max-width: 7ch' class='limiteColuna'>" + m.CodCliente + "</td>";
                conteudo += "<td style='max-width: 7ch' class='limiteColuna'>" + m.Codun + "</td>";
                conteudo += "<td style='max-width: 19ch' class='limiteColuna'>" + m.RazaoSocial + "</td>";
                conteudo += "<td style='max-width: 19ch' class='class='limiteColuna'>" + m.NomeFantasia + "</td>";
                conteudo += "<td style='max-width: 10ch' class='limiteColuna'>" + m.Classe + "</td>";
                conteudo += "<td style='max-width: 15ch' class='limiteColuna'>" + m.NomeAgente + "</td>";
                conteudo += "<td style='max-width: 15ch' class='limiteColuna'>" + m.NomePromotor + "</td>";
                conteudo += "<td>";
                conteudo += "<a href='/AreaRestrita/Cliente/Visualizacao/" + m.IdCliente + "' target='_blank' class='btn btn-success btn-xs'>Detalhes</a>";
                conteudo += "&nbsp;";
                conteudo += "<a href='/AreaRestrita/Cliente/Cadastro/" + m.IdCliente + "' class='btn btn-primary btn-xs'>Editar</a>";
                conteudo += "</td>";

                conteudo += "</tr>";
            });
            $("#tabela tbody").html(conteudo);
            $('#tabela').DataTable();
        },
        error: function (e) {
            alert(e.status);
        },
        complete: function () {
            $('#btnConsultar').prop('value', 'Consultar').prop('disabled', false);
        }
    });
};