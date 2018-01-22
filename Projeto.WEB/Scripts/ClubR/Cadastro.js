$(document).ready(function () {

    $('#btnCadastrar').click(function () {
        var formData = new FormData($("#uplFile")[0]);
        var model = {
            NumeroContrato: 1,
            Codun: $('#txtCodun').val(),
            RazaoSocial: $("#txtRazaoSocial").val(),
            NomeResponsavel: $("#txtNomeResponsavel").val(),
            CpfResponsavel: $("#txtCpfResponsavel").val(),
            DataNegociacao: $("#txtDataNegociacao").val(),
            ModalidadeClubR: $("#optModalidade").val(),
            PeriodoMeses: $("#optPeriodo").val(),
            DataInicioContrato: $("#txtDataInicio").val(),
            DataFimContrato: $("#txtDataFim").val(),
            MediaHistorica: $("#txtMediaHistorica").val(),
            MetaPeriodo: $("#txtMetaPeriodo").val(),
            CrescimentoProposto: $("#txtCrescimentoProposto").val(),
            PrazoPagamento: $("#optPrazoPagamento").val(),
            Desconto: $("#optDesconto").val(),
            RebatePercent: $("#txtRebatePercent").val(),
            RebateValor: $("#txtRebateValor").val(),
            Contrato: formData,
            Obervacao: $("#txtObservacao").val(),
            Ativo: true
        };
        $.ajax({
            type: 'POST',
            url: '/AreaRestrita/ClubR/Cadastro',
            dataType: 'JSON',
            data: model,
            success: function (data) {
                alert("OK");
            },
            error: function (e) {
                console.log(e.status);
            }
        });
    });

    $('.money').mask('000.000.000.000.000,00', { reverse: true });

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

        Rebate();
    }
}

function Rebate() {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/ClubR/Rebate',
        data: model = {
            ModalidadeClubR: $("#optModalidade").val(),
            CrescimentoProposto: (($("#txtCrescimentoProposto").val().replace('%', '') / 100).toFixed(3)).replace('.', ',')
        },
        success: function (data) {
            $.each(data, function (i, d) {
                $("#txtRebatePercent").val((d.RebatePercent * 100) + "%");
                $("#txtRebateValor").val(parseFloat($("#txtMetaPeriodo").val()) * d.RebatePercent);
            });
        },
        error: function (e) {
            console.log(e.status);
        }
    });
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