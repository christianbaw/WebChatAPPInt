const username = userName;
const textInput = $('#messageText');

document.getElementById('submitButton').addEventListener('click', () => {
    let text = textInput.val();
    $.post("api/message", { text }).done(saveMessageResponse);
});

function saveMessageResponse(response) {
    textInput.val('');
    sendMessageToHub(response);
}

function addMessageToChat(message) {
    let container = $('<div class="row REPLACEME">');
    let usermessage = (username == message.userName) ? '<text> justify-content-end </text>' : '<text> justify-content-start </text>';
    let template = '<div class="col-lg-3">CONTENT</div>';
    container.append($(usermessage.replace('REPLACEME', usermessage)));
    container.append($(template.replace('CONTENT', message.userName)));
    container.append($(template.replace('CONTENT', message.text)));
    //container.append($(template.replace('CONTENT', message.messageDate)));
    $('#chat').append(container);
}
