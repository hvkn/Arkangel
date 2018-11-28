const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name: String,
    user : String,
    enableFTP : Boolean,
    hoursFTP : Number,
    minutesFTP : Number,
    enableUploadKeystroke : Boolean,
    enableUploadImage : Boolean,
    enableUploadWebcam : Boolean,
    enableUploadWebsite : Boolean,
    enableLimit : Boolean,
    limitFTP : String,
    enableClear : Boolean,
    enablePassive : Boolean,
    hostnameFTP : String,
    userFTP : String,
    passwordFTP : String,
    remoteDirFTP : String
},
    { collection: "settings" });
module.exports = mongoose.model('modelFTP', schema);