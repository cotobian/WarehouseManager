import 'package:flutter/material.dart';

class ImportTally extends StatefulWidget {
  @override
  State<ImportTally> createState() => _ImportTallyState();
}

class _ImportTallyState extends State<ImportTally> {
  final TextEditingController _poController = TextEditingController();
  final TextEditingController _itemController = TextEditingController();
  final TextEditingController _quantityController = TextEditingController();
  final TextEditingController _remainController = TextEditingController();
  final TextEditingController _palletController = TextEditingController();
  final int ReceiptDetailId = 0;
  final List<String> items = [
    'Item 1',
    'Item 2',
    'Item 3',
    'Item 4',
    'Item 5',
    'Item 6',
    'Item 7',
  ];

  void displayDialog(context, title, text) => showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text(title),
          content: Text(text),
        ),
      );

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width - 20;
    return Scaffold(
      body: Padding(
        padding: EdgeInsets.all(10),
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: <Widget>[
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  Text(
                    'Tạo job nhập',
                    style: TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.start,
                children: <Widget>[
                  SizedBox(
                    width: screenWidth / 2,
                    child: Container(
                      child: TextField(
                        controller: _poController,
                        decoration: InputDecoration(
                          labelText: 'Số PO',
                        ),
                      ),
                    ),
                  ),
                  SizedBox(
                    width: screenWidth / 2,
                    child: Container(
                      child: TextField(
                        controller: _itemController,
                        decoration: InputDecoration(
                          labelText: 'Số Item',
                        ),
                      ),
                    ),
                  ),
                ],
              ),
              SizedBox(
                height: 10,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ElevatedButton(
                    onPressed: () async {
                      String po = _poController.text;
                      String item = _itemController.text;
                    },
                    child: Text("Tìm kiếm"),
                  )
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.start,
                children: <Widget>[
                  SizedBox(
                    width: screenWidth / 2,
                    child: Container(
                      child: TextField(
                        controller: _quantityController,
                        decoration: InputDecoration(
                          labelText: 'Số lượng',
                        ),
                      ),
                    ),
                  ),
                  SizedBox(
                    width: screenWidth / 2,
                    child: Container(
                      child: TextField(
                        controller: _remainController,
                        decoration: InputDecoration(
                          labelText: 'Còn lại',
                        ),
                      ),
                    ),
                  ),
                ],
              ),
              SizedBox(
                height: 10,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ElevatedButton(
                    onPressed: () async {
                      String po = _poController.text;
                      String item = _itemController.text;
                    },
                    child: Text("Thêm chi tiết"),
                  )
                ],
              ),
              SizedBox(
                height: 10,
              ),
              Container(
                decoration: BoxDecoration(
                  border: Border.all(
                    color: Colors.blue,
                    width: 1.0,
                  ),
                  borderRadius: BorderRadius.circular(8.0),
                ),
                child: SingleChildScrollView(
                  child: SizedBox(
                    height: 200,
                    child: ListView.builder(
                      itemCount: items.length,
                      itemBuilder: (context, index) {
                        return ListTile(
                          title: Text(items[index]),
                        );
                      },
                    ),
                  ),
                ),
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.start,
                children: <Widget>[
                  SizedBox(
                    width: screenWidth / 2,
                    child: TextField(
                      controller: _palletController,
                      decoration: InputDecoration(
                        contentPadding: EdgeInsets.all(10),
                        labelText: 'Số pallet',
                      ),
                    ),
                  ),
                  ElevatedButton(
                    onPressed: () async {
                      String po = _poController.text;
                      String item = _itemController.text;
                    },
                    child: Text("Tạo pallet"),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}
