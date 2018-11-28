const mongoose = require('mongoose');
var schema = new mongoose.Schema({time: String, user : String, content: String, color: String, css: String},{collection : "clipboardLog"});
module.exports = mongoose.model('modelFindClipboardLog', schema);