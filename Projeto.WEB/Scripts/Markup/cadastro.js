$(document).ready(function () {

    $("#btnCadastrar").prop('value', 'Cadastrar').prop('disabled', false);


    $("#txtCodun").change(function () {
        $.ajax({
            type: 'POST',
            url: '/AreaRestrita/Markup/Versao',
            data: model = {
                Codun: $("#txtCodun").val()
            },
            success: function (versao) {
                $("#txtVersao").val(versao);
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
        MarkUp();
        Desconto();
        PrazoPagamento();
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

    $("#formMarkupCadastro").submit(function () {
        $('#btnCadastrar').prop('value', 'Aguarde...').prop('disabled', true);
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
        url: '/AreaRestrita/Markup/MetaMinima',
        data: model = {
            ModalidadeMarkup: $("#optModalidade").val()
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

function PrazoPagamento() {

    var selectbox = $('#optPrazoPagamentoRBR');
    selectbox.find('option').remove();
    $('<option>').val("").text("-Selecione-").appendTo(selectbox);

    //Se markup 5
    if ($('#optModalidade').val() == "3") {
        $('<option>').val(1).text("30 Dias").appendTo(selectbox);
        $('<option>').val(2).text("2 Vezes").appendTo(selectbox);
        $('<option>').val(3).text("3 Vezes").appendTo(selectbox);
        $('<option>').val(4).text("4 Vezes").appendTo(selectbox);
    }
    else {
        $('<option>').val(1).text("30 Dias").appendTo(selectbox);
        $('<option>').val(2).text("2 Vezes").appendTo(selectbox);
        $('<option>').val(3).text("3 Vezes").appendTo(selectbox);
        $('<option>').val(4).text("4 Vezes").appendTo(selectbox);
        $('<option>').val(5).text("5 Vezes").appendTo(selectbox);
        $('<option>').val(6).text("6 Vezes").appendTo(selectbox);
        $('<option>').val(7).text("7 Vezes").appendTo(selectbox);
        $('<option>').val(8).text("8 Vezes").appendTo(selectbox);
        $('<option>').val(9).text("9 Vezes").appendTo(selectbox);
    }
}

function MarkUp() {
    var mkp = $('#optModalidade').val();

    switch (mkp) {
        case "1":
            $('#txtMarkUP').val("4,3")
            break;
        case "2":
            $('#txtMarkUP').val("4,8")
            break;
        case "3":
            $('#txtMarkUP').val("5")
            break;
    } 
}