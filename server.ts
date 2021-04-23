import * as express from "express"
import * as bodyParser from "body-parser"

let boardState = "0,0-Red-nox2,0-Red-nox4,0-Red-nox6,0-Red-nox1,1-Red-nox3,1-Red-nox5,1-Red-nox7,1-Red-nox0,2-Red-nox2,2-Red-nox4,2-Red-nox6,2-Red-nox1,5-Blk-nox3,5-Blk-nox5,5-Blk-nox7,5-Blk-nox0,6-Blk-nox2,6-Blk-nox4,6-Blk-nox6,6-Blk-nox1,7-Blk-nox3,7-Blk-nox5,7-Blk-nox7,7-Blk-no"
let turn = "red"


const app = express()
app.use(bodyParser.json())
 
app.get('/get_checkerboard', 
    function (request, response) 
    {
        response.send(boardState + "t" + turn)
    }
)

app.post(/\/set_checkerboard\/(.*)\/(.*)/, 
    function (request, response) 
    {
        const request_url = request.url

        const checker_board_state = request_url.match(/\/set_checkerboard\/(.*)\/(.*)/)[1]
        const checker_board_turn = request_url.match(/\/set_checkerboard\/(.*)\/(.*)/)[2]
        boardState = checker_board_state
        turn = checker_board_turn
        response.send()
    }
)
 
app.listen(9000)