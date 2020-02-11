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
    let usermessage = (username == message.userName) ? 'justify-content-end' : 'justify-content-start';
    let container = $('<div class="row ' + usermessage + '">');
    let template = '<div class="col-lg-3">CONTENT</div>';
    container.append($(template.replace('CONTENT', '<span class="helper-tag">' + message.userName + '</span>' + '<p class="message-tag">' + message.text + '</p>' + '<span class="helper-tag-italic">' + message.messageDate + '</span>' )));
    $('#chat').append(container);
}
