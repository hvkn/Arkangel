const mongoose = require('mongoose');
var schema = new mongoose.Schema({time: String, user : String, content: String, color: String, css: String},{collection : "keystroke"});
module.exports = mongoose.model('modelFindKeystroke', schema);