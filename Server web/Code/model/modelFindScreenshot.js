const mongoose = require('mongoose');
var schema = new mongoose.Schema({date: String, user : String, time: Array},{collection : "screenshot"});
module.exports = mongoose.model('modelFindScreenshot', schema);