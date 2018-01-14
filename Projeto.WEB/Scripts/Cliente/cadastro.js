$(document).ready(function () {

    $('#btnSalvar').click(function () {
        idCliente = $('#idCliente').val();
        idEnderecoCadastro = $('#idEnderecoCadastro').val();
        idEnderecoCobranca = $('#idEnderecoCobranca').val();
        idEnderecoEntrega = $('#idEnderecoEntrega').val();
        url = "/AreaRestrita/Cliente/Edicao";
    });

    $('#btnCadastrar').click(function () {
        idCliente = 0;
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
            classe: { required: true },
            nomeRepresentante: { required: true },
            cnpj: { required: true, cnpj: true },
            codun: { digits: true },
            razaoSocial: { required: true },
            logradouro: { required: true },
            numero: { required: true },
            bairro: { required: true },
            municipio: { required: true },
            UF: { rangelength: [2, 2], required: true },
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
            classe: { required: 'Campo obrigatorio' },
            nomeRepresentante: { required: 'Campo obrigatorio' },
            cnpj: { required: 'Campo obrigatorio' },
            codun: { digits: 'Insira apenas números', required: 'Campo obrigatorio' },
            razaoSocial: { required: 'Campo obrigatorio' },
            logradouro: { required: 'Campo obrigatorio' },
            numero: { required: 'Campo obrigatorio' },
            bairro: { required: 'Campo obrigatorio' },
            municipio: { required: 'Campo obrigatorio' },
            UF: { rangelength: 'Informe a sigla de 2 dígitos', required: 'Obrigatorio' },
            cep: { required: 'Campo obrigatorio' },
            email: { email: 'E-mail inválido' },
            cobranca_logradouro: { required: 'Campo obrigatorio' },
            cobranca_numero: { required: 'Campo obrigatorio' },
            cobranca_bairro: { required: 'Campo obrigatorio' },
            cobranca_municipio: { required: 'Campo obrigatorio' },
            cobranca_UF: { rangelength: [2, 2], required: 'Campo obrigatorio' },
            cobranca_cep: { required: 'Campo obrigatorio' },
            entrega_logradouro: { required: 'Campo obrigatorio' },
            entrega_numero: { required: 'Campo obrigatorio' },
            entrega_bairro: { required: 'Campo obrigatorio' },
            entrega_municipio: { required: 'Campo obrigatorio' },
            entrega_UF: { rangelength: [2, 2], required: 'Campo obrigatorio' },
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
                idCliente: idCliente,
                codun: $('#txtCodun').val(),
                razaoSocial: $('#txtRazaoSocial').val(),
                nomeFantasia: $('#txtNomeFantasia').val(),
                cnpj: $('#txtCnpj').val().replace(/\.|-|[/]/g, ''),
                inscricaoEstadual: $('#txtInscEstadual').val(),
                inscricaoMunicipal: $('#txtInscMunicipal').val(),
                classe: $('#optClasse').val(),
                idRepresentante: $('#optRepresentante').val(),
                enderecoCadastro: {
                    idEndereco: idEnderecoCadastro,
                    tipo: 'Cadastro',
                    logradouro: $('#cadastro_Logradouro').val(),
                    numero: $('#cadastro_Numero').val(),
                    complemento: $('#cadastro_Complemento').val(),
                    bairro: $('#cadastro_Bairro').val(),
                    municipio: $('#cadastro_Municipio').val(),
                    UF: $('#cadastro_UF').val(),
                    cep: $('#cadastro_CEP').val(),
                    email: $('#cadastro_Email').val(),
                    telefone1: $('#cadastro_Telefone1').val(),
                    telefone2: $('#cadastro_Telefone2').val()
                },
                enderecoCobranca: {
                    idEndereco: idEnderecoCobranca,
                    tipo: 'Cobranca',
                    logradouro: $('#cobranca_Logradouro').val(),
                    numero: $('#cobranca_Numero').val(),
                    complemento: $('#cobranca_Complemento').val(),
                    bairro: $('#cobranca_Bairro').val(),
                    municipio: $('#cobranca_Municipio').val(),
                    UF: $('#cobranca_UF').val(),
                    cep: $('#cobranca_CEP').val(),
                    email: $('#cobranca_Email').val(),
                    telefone1: $('#cobranca_Telefone1').val(),
                    telefone2: $('#cobranca_Telefone2').val()
                },
                enderecoEntrega: {
                    idEndereco: idEnderecoEntrega,
                    tipo: 'Entrega',
                    logradouro: $('#entrega_Logradouro').val(),
                    numero: $('#entrega_Numero').val(),
                    complemento: $('#entrega_Complemento').val(),
                    bairro: $('#entrega_Bairro').val(),
                    municipio: $('#entrega_Municipio').val(),
                    UF: $('#entrega_UF').val(),
                    cep: $('#entrega_CEP').val(),
                    email: $('#entrega_Email').val(),
                    telefone1: $('#entrega_Telefone1').val(),
                    telefone2: $('#entrega_Telefone2').val()
                }
            };
            $.ajax({
                type: "POST",
                url: url,
                data: model,
                success: function (mensagem) {
                    alert(mensagem);
                },
                Error: function (e) {
                    alert(e.message);
                }
            });
        }
    });

    $('#btnConsultaCNPJ').click(function () {

        var cnpj = $('#txtCnpj').val().replace(/\.|-|[/]/g, '');

        var rgx = new RegExp("^[0-9]{14}$");
        if (rgx.test(cnpj)) {

            $.ajax({
                type: 'POST',
                url: '/AreaRestrita/Cliente/ConsultarCNPJ',
                data: model = {
                    cnpj: cnpj
                },
                beforeSend: function () {
                    $('#btnConsultaCNPJ').prop('value', 'Aguarde...').prop('disabled', true);
                },
                success:
                function (m) {
                    if (m.status !== "ERROR") {
                        $("#txtRazaoSocial").val(m.razaoSocial);
                        $("#txtNomeFantasia").val(m.nomeFantasia);
                        $("#cadastro_Logradouro").val(m.enderecoCadastro.logradouro);
                        $("#cadastro_Numero").val(m.enderecoCadastro.numero);
                        $("#cadastro_Complemento").val(m.enderecoCadastro.complemento);
                        $("#cadastro_Bairro").val(m.enderecoCadastro.bairro);
                        $("#cadastro_Municipio").val(m.enderecoCadastro.municipio);
                        $("#cadastro_UF").val(m.enderecoCadastro.uf);
                        $("#cadastro_CEP").val(m.enderecoCadastro.cep);
                        $("#cadastro_Email").val(m.enderecoCadastro.email);
                        $("#cadastro_Telefone1").val(m.enderecoCadastro.telefone1);
                    }
                    else {
                        alert("CNPJ não encontrado!");
                    }
                },
                error: function (event) {
                    alert(event.message);
                },
                complete: function () {
                    $('#btnConsultaCNPJ').prop('value', 'Consultar pelo CNPJ').prop('disabled', false);
                }
            });
        }
        else {
            alert("CNPJ inválido, informe apenas caracteres numéricos com 14 caractéres");
        }
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