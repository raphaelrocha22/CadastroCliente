$(document).ready(function () {

    $('#btnSalvar').click(function () {
        idCliente = $('#idCliente').val();
        codCliente = $('#txtCodCliente').val();
        idEnderecoCadastro = $('#idEnderecoCadastro').val();
        idEnderecoCobranca = $('#idEnderecoCobranca').val();
        idEnderecoEntrega = $('#idEnderecoEntrega').val();
        url = "/AreaRestrita/Cliente/Edicao";
    });

    $('#btnCadastrar').click(function () {
        idCliente = 0;
        codCliente = 0;
        idEnderecoCadastro = 0;
        idEnderecoCobranca = 0;
        idEnderecoEntrega = 0;
        url = "/AreaRestrita/Cliente/Cadastro";
    });

    jQuery.validator.addMethod("cnpj", function (value, element) {
        var rgx = new RegExp("^[0-9]{14}$");
        return rgx.test(value.replace(/\.|-|[/]/g, ''));
    }, "Formato inválido. Digite apenas números de 14 caractéres");

    $('#formCadastroCliente').validate({
        errorClass: "my-error-class",
        validClass: "my-valid-class",
        rules: {
            classe: { required: true, min: 1 },
            representante: { required: true, min: 1 },
            cnpj: { required: true, cnpj: true },
            codun: { digits: true },
            codCliente: { digits: true },
            razaoSocial: { required: true },
            logradouro: { required: true },
            numero: { required: true },
            bairro: { required: true },
            municipio: { required: true },
            uf: { rangelength: [2, 2], required: true },
            cep: { required: true },
            email: { email: true },
            cobranca_logradouro: { required: ".cobranca:enabled" },
            cobranca_numero: { required: ".cobranca:enabled" },
            cobranca_bairro: { required: ".cobranca:enabled" },
            cobranca_municipio: { required: ".cobranca:enabled" },
            cobranca_UF: { rangelength: [2, 2], required: ".cobranca:enabled" },
            cobranca_cep: { required: ".cobranca:enabled" },
            entrega_logradouro: { required: ".entrega:enabled" },
            entrega_numero: { required: ".entrega:enabled" },
            entrega_bairro: { required: ".entrega:enabled" },
            entrega_municipio: { required: ".entrega:enabled" },
            entrega_UF: { rangelength: [2, 2], required: ".entrega:enabled" },
            entrega_cep: { required: ".entrega:enabled" }
        },
        messages: {
            classe: { required: 'Campo obrigatorio', min: 'Campo obrigatorio' },
            representante: { required: 'Campo obrigatorio', min: 'Campo obrigatorio' },
            cnpj: { required: 'Campo obrigatorio' },
            codun: { digits: 'Insira apenas números' },
            codCliente: { digits: 'Insira apenas números' },
            razaoSocial: { required: 'Campo obrigatorio' },
            logradouro: { required: 'Campo obrigatorio' },
            numero: { required: 'Campo obrigatorio' },
            bairro: { required: 'Campo obrigatorio' },
            municipio: { required: 'Campo obrigatorio' },
            uf: { rangelength: 'Informe a sigla de 2 dígitos', required: 'Obrigatorio' },
            cep: { required: 'Campo obrigatorio' },
            email: { email: 'E-mail inválido' },
            cobranca_logradouro: { required: 'Campo obrigatorio' },
            cobranca_numero: { required: 'Campo obrigatorio' },
            cobranca_bairro: { required: 'Campo obrigatorio' },
            cobranca_municipio: { required: 'Campo obrigatorio' },
            cobranca_UF: { rangelength: 'Informe a sigla de 2 dígitos', required: 'Obrigatorio' },
            cobranca_cep: { required: 'Campo obrigatorio' },
            entrega_logradouro: { required: 'Campo obrigatorio' },
            entrega_numero: { required: 'Campo obrigatorio' },
            entrega_bairro: { required: 'Campo obrigatorio' },
            entrega_municipio: { required: 'Campo obrigatorio' },
            entrega_UF: { rangelength: 'Informe a sigla de 2 dígitos', required: 'Obrigatorio' },
            entrega_cep: { required: 'Campo obrigatorio' }
        },
        submitHandler: function (form) {
            if ($('#check_Cobranca_Cadastro').prop('checked')) {
                $('#cobranca_Logradouro').val($('#cadastro_Logradouro').val());
                $('#cobranca_Numero').val($('#cadastro_Numero').val());
                $('#cobranca_Complemento').val($('#cadastro_Complemento').val());
                $('#cobranca_Bairro').val($('#cadastro_Bairro').val());
                $('#cobranca_Municipio').val($('#cadastro_Municipio').val());
                $('#cobranca_UF').val($('#cadastro_UF').val());
                $('#cobranca_CEP').val($('#cadastro_CEP').val());
                $('#cobranca_Email').val($('#cadastro_Email').val());
                $('#cobranca_Telefone1').val($('#cadastro_Telefone1').val());
                $('#cobranca_Telefone2').val($('#cadastro_Telefone2').val());
            }

            if ($('#check_Entrega_Cadastro').prop('checked')) {
                $('#entrega_Logradouro').val($('#cadastro_Logradouro').val());
                $('#entrega_Numero').val($('#cadastro_Numero').val());
                $('#entrega_Complemento').val($('#cadastro_Complemento').val());
                $('#entrega_Bairro').val($('#cadastro_Bairro').val());
                $('#entrega_Municipio').val($('#cadastro_Municipio').val());
                $('#entrega_UF').val($('#cadastro_UF').val());
                $('#entrega_CEP').val($('#cadastro_CEP').val());
                $('#entrega_Email').val($('#cadastro_Email').val());
                $('#entrega_Telefone1').val($('#cadastro_Telefone1').val());
                $('#entrega_Telefone2').val($('#cadastro_Telefone2').val());
            }
            else if ($('#check_Entrega_Cobranca').prop('checked')) {
                $('#entrega_Logradouro').val($('#cobranca_Logradouro').val());
                $('#entrega_Numero').val($('#cobranca_Numero').val());
                $('#entrega_Complemento').val($('#cobranca_Complemento').val());
                $('#entrega_Bairro').val($('#cobranca_Bairro').val());
                $('#entrega_Municipio').val($('#cobranca_Municipio').val());
                $('#entrega_UF').val($('#cobranca_UF').val());
                $('#entrega_CEP').val($('#cobranca_CEP').val());
                $('#entrega_Email').val($('#cobranca_Email').val());
                $('#entrega_Telefone1').val($('#cobranca_Telefone1').val());
                $('#entrega_Telefone2').val($('#cobranca_Telefone2').val());
            }

            var model = {
                IdCliente: idCliente,
                Codun: $('#txtCodun').val(),
                CodCliente: codCliente,
                RazaoSocial: $('#txtRazaoSocial').val(),
                NomeFantasia: $('#txtNomeFantasia').val(),
                Cnpj: $('#txtCnpj').val().replace(/\.|-|[/]/g, ''),
                InscricaoEstadual: $('#txtInscEstadual').val(),
                InscricaoMunicipal: $('#txtInscMunicipal').val(),
                Classe: $('#optClasse').val(),
                IdRepresentante: $('#optRepresentante').val(),
                EnderecoCadastro: {
                    IdEndereco: idEnderecoCadastro,
                    Tipo: 'Cadastro',
                    Logradouro: $('#cadastro_Logradouro').val(),
                    Numero: $('#cadastro_Numero').val(),
                    Complemento: $('#cadastro_Complemento').val(),
                    Bairro: $('#cadastro_Bairro').val(),
                    Municipio: $('#cadastro_Municipio').val(),
                    UF: $('#cadastro_UF').val(),
                    Cep: $('#cadastro_CEP').val(),
                    Email: $('#cadastro_Email').val(),
                    Telefone1: $('#cadastro_Telefone1').val(),
                    Telefone2: $('#cadastro_Telefone2').val()
                },
                EnderecoCobranca: {
                    IdEndereco: idEnderecoCobranca,
                    Tipo: 'Cobranca',
                    Logradouro: $('#cobranca_Logradouro').val(),
                    Numero: $('#cobranca_Numero').val(),
                    Complemento: $('#cobranca_Complemento').val(),
                    Bairro: $('#cobranca_Bairro').val(),
                    Municipio: $('#cobranca_Municipio').val(),
                    UF: $('#cobranca_UF').val(),
                    Cep: $('#cobranca_CEP').val(),
                    Email: $('#cobranca_Email').val(),
                    Telefone1: $('#cobranca_Telefone1').val(),
                    Telefone2: $('#cobranca_Telefone2').val()
                },
                EnderecoEntrega: {
                    IdEndereco: idEnderecoEntrega,
                    Tipo: 'Entrega',
                    Logradouro: $('#entrega_Logradouro').val(),
                    Numero: $('#entrega_Numero').val(),
                    Complemento: $('#entrega_Complemento').val(),
                    Bairro: $('#entrega_Bairro').val(),
                    Municipio: $('#entrega_Municipio').val(),
                    UF: $('#entrega_UF').val(),
                    Cep: $('#entrega_CEP').val(),
                    Email: $('#entrega_Email').val(),
                    Telefone1: $('#entrega_Telefone1').val(),
                    Telefone2: $('#entrega_Telefone2').val()
                }
            };
            $.ajax({
                type: "POST",
                url: url,
                data: model,
                beforeSend: function () {
                    $('#btnCadastrar').prop('value', 'Aguarde...').prop('disabled', true);
                },
                success: function (mensagem) {
                    alert(mensagem);
                },
                error: function (e) {
                    alert(e.message);
                },
                complete: function () {
                    $('#btnCadastrar').prop('value', 'Cadastrar').prop('disabled', false);
                }
            });
        }
    });

    $('#btnConsultaCNPJ').click(function () {
        var cnpj = $('#txtCnpj').val().replace(/\.|-|[/]/g, '');
        ConsultarCNPJ(cnpj);
    });

    $('#check_Cobranca_Cadastro').click(function () {
        if (this.checked) {
            $('.cobranca').prop('disabled', true);
            $('#check_Entrega_Cobranca').prop('checked', false).prop('disabled', true);
        }
        else {
            $('.cobranca').prop('disabled', false);
            $('#check_Entrega_Cobranca').prop('disabled', false);
        }
    });

    $('#check_Entrega_Cadastro').click(function () {
        if (this.checked) {
            $('.entrega').prop('disabled', true);
            $('#check_Entrega_Cobranca').prop('checked', false);
        }
        else {
            $('.entrega').prop('disabled', false);
            $('#check_Entrega_Cobranca').prop('disabled', false);
        }
    });

    $('#check_Entrega_Cobranca').click(function () {
        if (this.checked) {
            $('.entrega').prop('disabled', true);
            $('#check_Entrega_Cadastro').prop('checked', false);
            $('#check_Cobranca_Cadastro').prop('checked', false).prop('disabled', true);
            $('.cobranca').prop('disabled', false);
        }
        else {
            $('.entrega').prop('disabled', false);
            $('#check_Entrega_Cadastro').prop('disabled', false);
            $('#check_Cobranca_Cadastro').prop('disabled', false);
        }
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
};