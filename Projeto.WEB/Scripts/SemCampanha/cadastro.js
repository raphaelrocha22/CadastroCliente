$(document).ready(function () {

    $("#btnCadastrar").prop('value', 'Cadastrar').prop('disabled', false);

    if ($("#txtIdTransacao").val() != 0) {
        $("#txtCodun").prop('readonly', true);
        $("#txtCodCliente").prop('readonly', true);
    };
    
    $("#txtCodun").change(function () {
        $("#txtCodCliente").val(0);
        VerificarNumeroContrato();
    });

    $("#txtCodCliente").change(function () {
        $("#txtCodun").val(0);
        VerificarNumeroContrato();
    });

    $('.date').datepicker({
    });

    $('#optPrazoPagamentoRBR').change(function () {
        ValidarMarkUP();
    });

    $('#txtMarkUP').change(function () {
        $('#txtMarkUP').val($(this).val().replace('.', ','));
        ValidarMarkUP();
        Desconto();
    });

    $("#formSemCampanhaCadastro").submit(function () {
        $('#btnCadastrar').prop('value', 'Aguarde...').prop('disabled', true);
    });
});

function VerificarNumeroContrato() {
    $.ajax({
        type: 'POST',
        url: '/AreaRestrita/SemCampanha/NumeroContrato',
        data: model = {
            Codun: $("#txtCodun").val(),
            CodCliente: $("#txtCodCliente").val(),
            IdTransacao: $("#txtIdTransacao").val(),
        },
        success: function (numeroContrato) {
            $("#txtNumeroContrato").val(numeroContrato);
        },
        error: function (e) {
            console.log(e.status);
        }
    });
}

function Desconto() {
    if ($('#txtMarkUP').val() !== "") {
        var mkup = $('#txtMarkUP').val().replace(',', '.');
        var desconto = (1 - (2.52 / mkup)) * 100;
        $("#txtDesconto").val(desconto.toFixed(1) + "%");
    }
}

function ValidarMarkUP() {
    var pagamento = parseInt($('#optPrazoPagamentoRBR').val());
    var markup = $('#txtMarkUP').val().replace(',', '.');

    if (pagamento == 1) {
        if (markup > 4.4) {
            alert('O maior MarkUP disponível para o prazo de pagamento escolhido é de 4,4')
            $('#txtMarkUP').val("");
            $('#txtDesconto').val("");
        }
    };
    if (pagamento > 1 && pagamento < 5) {
        if (markup > 4.2) {
            alert('O maior MarkUP disponível para o prazo de pagamento escolhido é de 4,2')
            $('#txtMarkUP').val("");
            $('#txtDesconto').val("");
        }
    };
    if (pagamento > 4) {
        if (markup > 4.0) {
            alert('O maior MarkUP disponível para o prazo de pagamento escolhido é de 4,0')
            $('#txtMarkUP').val("");
            $('#txtDesconto').val("");
        }
    };
}