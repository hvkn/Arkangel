const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    name : String, 
    user : String, 
    enableEmail: Boolean, 
    enableScreenshot: Boolean
},{collection : "settings"});
module.exports = mongoose.model('modelAlert', schema);