$(document).ready(function () {

    $('#formCadastroCliente').validate({
        rules: {
            obrigatorio: { required: true },
            email: { email: true }
        },
        messages: {
            obrigatorio: { required: 'Campo obrigatorio' },
            email: { email: 'Ops, informe um email válido' }
        },
        submitHandler: function (form) {
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
        }
        else {
            $('.cobranca').prop('disabled', false);
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
        }
        else {
            $('.entrega').prop('disabled', false);
            $('#check_Entrega_Cadastro').prop('disabled', false);
        }
    });
});