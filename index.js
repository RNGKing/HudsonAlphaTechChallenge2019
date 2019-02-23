const {app, BrowserWindow} = require('electron');
const ipc = require('electron').ipcMain

let win;

function createWindow(){
    win = new BrowserWindow({width:800, height: 600, frame:false});

    win.loadFile('./FrontEnd/index.html');
    win.webContents.openDevTools()
    console.log("starting file");

    win.on('closed', () =>{
        win = null;
    });

    
}



app.on('ready', createWindow);

app.on('window-all-closed', ()=>{
    if(win === null){
        createWindow();
    }
});

app.on('activate', () =>{
    if(win === null){
        createWindow();
    }
});
