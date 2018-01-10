$(document).ready(function () {

    $('#formCadastroCliente').validate({
        errorClass: "my-error-class",
        validClass: "my-valid-class",
        rules: {
            classe: { required: true },
            representante: { required: true },
            cnpj: { required: true },
            codun: { digits: true, required: true },
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
            representante: { required: 'Campo obrigatorio' },
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
                classe: $('#optClasse').val(),
                representante: $('#optRepresentante').val(),
                cnpj: $('#txtCnpj').val(),
                codun: $('#txtCodun').val(),
                nome: $('#txtRazaoSocial').val(),
                fantasia: $('#txtNomeFantasia').val(),
                inscricaoEstadual: $('#txtInscEstadual').val(),
                inscricaoMunicipal: $('#txtInscMunicipal').val(),
                enderecoCadastro:{
                    Logradouro: $('#cadastro_Logradouro').val(),
                    Numero: $('#cadastro_Numero').val(),
                    Complemento: $('#cadastro_Complemento').val(),
                    Bairro: $('#cadastro_Bairro').val(),
                    Municipio: $('#cadastro_Municipio').val(),
                    Uf: $('#cadastro_UF').val(),
                    Cep: $('#cadastro_CEP').val(),
                    Email: $('#cadastro_Email').val(),
                    Telefone1: $('#cadastro_Telefone1').val(),
                    Telefone2: $('#cadastro_Telefone2').val()
                },
                enderecoCobranca: {
                    Logradouro: $('#cobranca_Logradouro').val(),
                    Numero: $('#cobranca_Numero').val(),
                    Complemento: $('#cobranca_Complemento').val(),
                    Bairro: $('#cobranca_Bairro').val(),
                    Municipio: $('#cobranca_Municipio').val(),
                    Uf: $('#cobranca_UF').val(),
                    Cep: $('#cobranca_CEP').val(),
                    Email: $('#cobranca_Email').val(),
                    Telefone1: $('#cobranca_Telefone1').val(),
                    Telefone2: $('#cobranca_Telefone2').val()
                },
                enderecoEntrega: {
                    Logradouro: $('#entrega_Logradouro').val(),
                    Numero: $('#entrega_Numero').val(),
                    Complemento: $('#entrega_Complemento').val(),
                    Bairro: $('#entrega_Bairro').val(),
                    Municipio: $('#entrega_Municipio').val(),
                    Uf: $('#entrega_UF').val(),
                    Cep: $('#entrega_CEP').val(),
                    Email: $('#entrega_Email').val(),
                    Telefone1: $('#entrega_Telefone1').val(),
                    Telefone2: $('#entrega_Telefone2').val()
                }
            };
            $.ajax({
                type: "POST",
                url: "/AreaRestrita/Cliente/Cadastro",
                data: model,
                success: function (data) {
                    alert("SIM");
                },
                Error: function (e) {
                    alert(e.message)
                }
            });
            return false;
        }
    });
    
    $('#btnConsultaCNPJ').click(function () {
        
        var cnpj = $('#txtCnpj').val().replace(/\.|-|[/]/g, '')

        var rgx = new RegExp("^[0-9]{14}$");
        if (rgx.test(cnpj)) {

            $.ajax({
                type: 'POST',
                url: '/AreaRestrita/Cliente/ConsultarCNPJ',
                data: model = {
                    Cnpj: cnpj
                },
                success:
                function (m) {
                    if (m.Status != "ERROR") {
                        $("#txtRazaoSocial").val(m.Nome);
                        $("#txtNomeFantasia").val(m.Fantasia);
                        $("#cadastro_Logradouro").val(m.Logradouro);
                        $("#cadastro_Numero").val(m.Numero);
                        $("#cadastro_Complemento").val(m.Complemento);
                        $("#cadastro_Bairro").val(m.Bairro);
                        $("#cadastro_Municipio").val(m.Municipio);
                        $("#cadastro_UF").val(m.Uf);
                        $("#cadastro_CEP").val(m.Cep);
                        $("#cadastro_Email").val(m.Email);
                        $("#cadastro_Telefone1").val(m.Telefone1);

                    }
                    else {
                        alert("CNPJ não encontrado!")
                    }
                },
                error: function (event) {
                    alert(event.message);
                }
            });
        }
        else {
            alert("CNPJ inválido, informe apenas caracteres numéricos com 14 caractéres")
        }
    });

    $('#check_Cobranca_Cadastro').click(function () {
        if ((this).checked) {
            $('.cobranca').prop('disabled', true);
            $('#check_Entrega_Cobranca').prop('checked', false).prop('disabled', true);
        }
        else {
            $('.cobranca').prop('disabled', false);
            $('#check_Entrega_Cobranca').prop('disabled', false);
        }
    });

    $('#check_Entrega_Cadastro').click(function () {
        if ((this).checked) {
            $('.entrega').prop('disabled', true);
            $('#check_Entrega_Cobranca').prop('checked', false);
        }
        else {
            $('.entrega').prop('disabled', false);
            $('#check_Entrega_Cobranca').prop('disabled', false);
        }
    });

    $('#check_Entrega_Cobranca').click(function () {
        if ((this).checked) {
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