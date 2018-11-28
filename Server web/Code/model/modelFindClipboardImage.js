const mongoose = require('mongoose');
var schema = new mongoose.Schema({date: String, user : String, time: Array},{collection : "clipboardImage"});
module.exports = mongoose.model('modelFindClipboardImage', schema);