const {dialog} = require('electron').remote;

document.querySelector("#btnLoadFile").addEventListener('click', function(event){
    dialog.showOpenDialog({
        properties:['openFile', 'multiSelections']
    },function(files){
        if(files !== undefined){
            console.log("files received " + files);
        }
    }
    );
});
