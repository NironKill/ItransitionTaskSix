var editor;
var connectionLoadCanvas;
var boardId;
var username;
var userRole;

$(document).ready(function () {
    var pathname = window.location.pathname;
    boardId = pathname.split('/').pop();
    startDrawing();
    startCommentConnection(boardId);
});

function startDrawing() {
    const settings = {
        wheelEventsEnabled: 'only-if-focused',
    };

    editor = new jsdraw.Editor(document.body, settings);
    const toolbar = editor.addToolbar();

    toolbar.addExitButton(() => {
        editor.remove();
        window.location.href = "/";
    });

    toolbar.addActionButton('|Clear|', () => {
        editor.remove();
        startDrawing();
    });

    toolbar.addActionButton('|Download|', () => {
        var jpgDataUrl = editor.toDataURL();
        console.log(jpgDataUrl);
        download(jpgDataUrl, `drawing-${boardId}.jpg`);
    });


    editor.getRootElement().style.height = '95vh';
    editor.getRootElement().style.border = '2px solid gray';

    const addToHistory = false;
    editor.dispatch(editor.setBackgroundStyle({
        autoresize: true,
    }), addToHistory);

    editor.notifier.on(jsdraw.EditorEventType.CommandDone, (evt) => {
        if (evt.kind !== jsdraw.EditorEventType.CommandDone) {
            throw new Error('Incorrect event type');
        }

        if (evt.command instanceof jsdraw.SerializableCommand) {
            postToServer(JSON.stringify({
                command: evt.command.serialize()
            }));
            saveNewSvg();
        } else {
            console.log('!', evt.command, 'instanceof jsdraw.SerializableCommand');
        }
    });

    editor.notifier.on(jsdraw.EditorEventType.CommandUndone, (evt) => {
        if (evt.kind !== jsdraw.EditorEventType.CommandUndone) {
            return;
        }

        if (!(evt.command instanceof jsdraw.SerializableCommand)) {
            console.log('Not serializable!', evt.command);
            return;
        }

        postToServer(JSON.stringify({
            command: jsdraw.invertCommand(evt.command).serialize()
        }));
        saveNewSvg();
    });
}

function startCommentConnection(boardId) {
    connectionLoadCanvas = new signalR.HubConnectionBuilder().withUrl("/hub/presentation", signalR.HttpTransportType.WebSockets).build();

    connectionLoadCanvas.on("UpdateDrawing", (drawingData) => {
        console.log("Received new command");
        processUpdates(drawingData);
    });

    connectionLoadCanvas.on("ReceiveUserJoinInfo", (username) => {
        console.log("Received new user joined: " + username);
        toastr.info(username + " joined the board");

    });
    connectionLoadCanvas.start().then(() => {
        connectionLoadCanvas.invoke("JoinBoard", boardId.toString(), username).then((msg) =>
            console.log(msg))
    }).catch(err => console.error(err.toString()));
}

const postToServer = async (commandData) => {
    if (userRole > 0) {
        try {
            connectionLoadCanvas.invoke("Draw", boardId.toString(), commandData).catch(function (err) {
                return console.error(err.toString());
            });
            console.log('Posted my data', commandData);
        } catch (e) {
            console.error('Error posting command', e);
        }
    }
};

const processUpdates = async (drawingData) => {
    debugger;
    try {
        const json = JSON.parse(drawingData);
        console.log(json);
        try {
            const command = jsdraw.SerializableCommand.deserialize(json.command, editor);
            await command.apply(editor);
            debugger;
        } catch (e) {
            console.warn('Error parsing command', e);
        }
    } catch (e) {
        console.error('Error fetching updates', e);
    }
};

function download(dataurl, filename) {
    const link = document.createElement("a");
    link.href = dataurl;
    link.download = filename;
    link.click();
}