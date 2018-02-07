$(document).ready(function () {

    $("#btnCadastrar").prop('value', 'Cadastrar').prop('disabled', false);


    $("#txtCodun").change(function () {
        $.ajax({
            type: 'POST',
            url: '/AreaRestrita/ClubR/NumeroContrato',
            data: model = {
                Codun: $("#txtCodun").val()
            },
            success: function (numeroContrato) {
                $("#txtNumeroContrato").val(numeroContrato);
            },
            error: function (e) {
                console.log(e.status);
            }
        });
    });

    $('.money').mask('000.000.000.000.000', { reverse: true });

    $('.date').datepicker({
    });

    $('#txtDataFim').change(function () {
        $('#txtDataFim').val("");
        CalcularDataFim();
    });

    $("#txtDataInicio").change(function () {
        CalcularDataFim();
    });

    $('#optModalidade').change(function () {
        PrazoContrato();
        $('#txtDataInicio').val('');
        $('#txtDataFim').val('');
        MetaMinima();
    });

    $('#optPeriodo').change(function () {
        Crescimento();
        CalcularDataFim();
        MetaMinima();
    });

    $('#txtMediaHistoria').change(function () {
        Crescimento();
    });

    $('#txtMetaPeriodo').change(function () {
        Crescimento();
        Rebate();
    });

    $('#optRebatePercent').change(function () {
        Rebate();
    });

    $('#optPrazoPagamentoRBR').change(function () {
        ValidarMarkUP();
    });

    $('#txtMarkUP').change(function () {
        $('#txtMarkUP').val($(this).val().replace('.', ','));
        ValidarMarkUP();
        Desconto();
    });

    $('#btnCadastrar').click(function () {
        $(this).prop('value', 'Aguarde...').prop('disabled', true);
    });
});

function CalcularDataFim() {
    if ($('#optPeriodo').val() !== "" && $("#txtDataInicio").val() !== "") {
        var d = $.datepicker.parseDate('dd/mm/yy', $("#txtDataInicio").val());
        d.setMonth(d.getMonth() + parseInt($("#optPeriodo").val()));
        d.setDate(d.getDate() - 1);
        $('#txtDataFim').datepicker('setDate', d);
    }
}

function PrazoContrato() {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/ClubR/PrazosContrato',
        data: model = {
            ModalidadeClubR: $("#optModalidade").val()
        },
        success: function (data) {
            var selectbox = $('#optPeriodo');
            selectbox.find('option').remove();
            $.each(data, function (i, d) {
                $('<option>').val(d.PeriodoMeses).text(d.PrazoContrato).appendTo(selectbox);
            });
        },
        error: function (e) {
            console.log(e.status);
        }
    });
}

function Crescimento() {
    if ($('#txtMediaHistoria').val() !== "" && $('#txtMetaPeriodo').val() !== "" && $('#optPeriodo').val() !== "") {
        var mediaMensalPeriodo = parseFloat($("#txtMetaPeriodo").val()) / parseFloat($("#optPeriodo").val());
        var mediaHistorica = parseFloat($("#txtMediaHistorica").val());
        var crescimento = (((mediaMensalPeriodo / mediaHistorica) - 1) * 100).toFixed(1);
        $("#txtCrescimentoProposto").val(crescimento + "%");
    }
}

function MetaMinima() {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/ClubR/MetaMinima',
        data: model = {
            ModalidadeClubR: $("#optModalidade").val()
        },
        success: function (data) {
            $.each(data, function (i, d) {

                var periodo = $('#optPeriodo').val();
                var MetaMinima = d.MinimoMensalPeriodo * periodo;

                $('#txtMetaPeriodo').attr('placeholder', 'Mínimo: ' + MetaMinima);
                $('#txtMediaHistorica').attr('placeholder', 'Minimo: ' + d.MediaHistorica);
            });
        },
        error: function (e) {
            console.log(e.status);
        }
    });
}

function Rebate() {
    if ($("#optRebatePercent").val() !== "" && $('#txtMetaPeriodo').val() !== "") {
        var rebatePercent = $("#optRebatePercent").val();
        var Meta = ($("#txtMetaPeriodo").val()).replace('.', '');
        $('#txtRebateValor').val(rebatePercent * Meta);
    }
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