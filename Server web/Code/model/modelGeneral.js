const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name : String,
    user : String,
    disableTaskManager: Boolean,
    disableRegistry: Boolean,
    enableEncrypt: Boolean,
    enableStartup: Boolean,
    enableHotkey: Boolean,
    hotkeyGeneral: String
},
    { collection: "settings" });
module.exports = mongoose.model('modelGeneral', schema);