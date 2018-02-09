$(document).ready(function () {

    $("#btnCadastrar").prop('value', 'Cadastrar').prop('disabled', false);

    $('#btnConsultaCNPJ').click(function () {
        var cnpj = $('#txtCnpj').val().replace(/\.|-|[/]/g, '');
        ConsultarCNPJ(cnpj);
    });

    if ($('#check_CobrancaIgualCadastro').prop('checked')) {
        $('.cobranca').prop('disabled', true);
        $('#check_EntregaIgualCobranca').prop('checked', false).prop('disabled', true);
    };

    if ($('#check_EntregaIgualCadastro').prop('checked') || $('#check_EntregaIgualCobranca').prop('checked')) {
        $('.entrega').prop('disabled', true);
    };
    
    $('#check_CobrancaIgualCadastro').click(function () {
        if (this.checked) {
            $('.cobranca').prop('disabled', true);
            $('#check_EntregaIgualCobranca').prop('checked', false).prop('disabled', true);
        }
        else {
            $('.cobranca').prop('disabled', false);
            $('#check_EntregaIgualCobranca').prop('disabled', false);
        }
    });

    $('#check_EntregaIgualCadastro').click(function () {
        if (this.checked) {
            $('.entrega').prop('disabled', true);
            $('#check_EntregaIgualCobranca').prop('checked', false);
        }
        else {
            $('.entrega').prop('disabled', false);
            $('#check_EntregaIgualCobranca').prop('disabled', false);
        }
    });

    $('#check_EntregaIgualCobranca').click(function () {
        if (this.checked) {
            $('.entrega').prop('disabled', true);
            $('#check_EntregaIgualCadastro').prop('checked', false);
            $('#check_CobrancaIgualCadastro').prop('checked', false).prop('disabled', true);
            $('.cobranca').prop('disabled', false);
        }
        else {
            $('.entrega').prop('disabled', false);
            $('#check_EntregaIgualCadastro').prop('disabled', false);
            $('#check_CobrancaIgualCadastro').prop('disabled', false);
        }
    });

    $('#btnCadastrarNovoCliente').click(function () {
        $(location).attr('href', '/AreaRestrita/Cliente/Cadastro/');
    });

    $('#btnCadastrarModalidadeComercial').click(function () {
        if ($("#idTransacao").html() != "") {
            var id = $("#idTransacao").html();
            $(location).attr('href', '/AreaRestrita/SemCampanha/Cadastro/' + id);
        }
        else {
            alert("O redirecionamento não pôde ser realizado, IdTransação não encontrado, por favor entre em contato com com a equipe Backoffice RJ");
        }
    });

    $("#formClienteCadastro").submit(function () {
        $('#btnCadastrar').prop('value', 'Aguarde...').prop('disabled', true);
    });

});

function ConsultarCNPJ(cnpj) {
    
    var rgx = new RegExp("^[0-9]{14}$");
    if (rgx.test(cnpj)) {

        $.ajax({
            type: 'POST',
            url: '/AreaRestrita/Cliente/ConsultarCNPJ',
            data: "cnpj=" + cnpj,
            beforeSend: function () {
                $('#btnConsultaCNPJ').prop('value', 'Aguarde...').prop('disabled', true);
            },
            success:
            function (m) {
                if (m.Status !== "ERROR") {
                    $("#txtRazaoSocial").val(m.RazaoSocial);
                    $("#txtNomeFantasia").val(m.NomeFantasia);
                    $("#cadastro_Logradouro").val(m.EnderecoCadastro.Logradouro);
                    $("#cadastro_Numero").val(m.EnderecoCadastro.Numero);
                    $("#cadastro_Complemento").val(m.EnderecoCadastro.Complemento);
                    $("#cadastro_Bairro").val(m.EnderecoCadastro.Bairro);
                    $("#cadastro_Municipio").val(m.EnderecoCadastro.Municipio);
                    $("#cadastro_UF").val(m.EnderecoCadastro.UF);
                    $("#cadastro_CEP").val(m.EnderecoCadastro.Cep);
                    $("#cadastro_Email").val(m.EnderecoCadastro.Email);
                    $("#cadastro_Telefone1").val(m.EnderecoCadastro.Telefone1);
                }
                else {
                    $('#formCadastroCliente').each(function () {
                        this.reset();
                    });
                    alert("CNPJ não encontrado!");
                }
            },
            error: function (e) {
                alert(e.message);
            },
            complete: function () {
                $('#btnConsultaCNPJ').prop('value', 'Consultar pelo CNPJ').prop('disabled', false);
            }
        });
    }
    else {
        alert("CNPJ inválido, informe apenas caracteres numéricos com 14 caractéres");
    }
}