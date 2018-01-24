$(document).ready(function () {

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
    $('.cpf').mask('000.000.000-00', { reverse: true });

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

    $('#optPrazoPagamento').change(function () {
        Descontos();
    });

    $('#optPrazoPagamento').val("Normal");
    Descontos();
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

function Descontos() {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/ClubR/Descontos',
        data: model = {
            PrazoPagamento: $("#optPrazoPagamento").val()
        },
        success: function (data) {
            var selectbox = $('#optDesconto');
            selectbox.find('option').remove();
            $.each(data, function (i, d) {
                $('<option>').val(d.Desconto).text(((d.Desconto) * 100).toFixed(1) + "%").appendTo(selectbox);
            });
        },
        error: function (e) {
            console.log(e.status);
        }
    });
}

function Crescimento() {

    if ($('#txtMediaHistoria').val() !== "" && $('#txtMetaPeriodo').val() !== "" && $('#optPeriodo').val() != "") {

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
                $('#txtMetaPeriodo').attr('placeholder', 'Mínimo: ' + d.MetaPeriodo);
                $('#txtMediaHistorica').attr('placeholder', 'Minimo: ' + d.MediaHistorica);
            });
        },
        error: function (e) {
            console.log(e.status);
        }
    });
}

function Rebate() {
    if ($("#optRebatePercent").val() != "" && $('#txtMetaPeriodo').val() != "") {
        var rebatePercent = $("#optRebatePercent").val();
        var Meta = ($("#txtMetaPeriodo").val()).replace('.', '');
        $('#txtRebateValor').val(rebatePercent * Meta);
    }
}