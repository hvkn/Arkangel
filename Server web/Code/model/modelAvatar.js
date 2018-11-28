const mongoose = require('mongoose');
var schema = new mongoose.Schema({
    user : String, 
    path : String
},{collection : "avatar"});
module.exports = mongoose.model('modelAvatar', schema);