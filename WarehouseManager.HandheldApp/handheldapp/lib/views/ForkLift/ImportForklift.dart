import 'package:flutter/material.dart';
import 'package:handheldapp/logics/ApiService.dart';
import 'package:handheldapp/models/CompleteForkliftJobVm.dart';

class ImportForklift extends StatefulWidget {
  @override
  State<ImportForklift> createState() => _ImportForkliftState();
}

class _ImportForkliftState extends State<ImportForklift> {
  final TextEditingController _palletController = TextEditingController();
  final TextEditingController _positionController = TextEditingController();
  final ScrollController _listController = ScrollController();
  List<String> items = [];

  void displayDialog(context, title, text) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(title),
        content: Text(text),
      ),
    );
  }

  @override
  void initState() {
    super.initState();
    initDataSearch();
  }

  void initDataSearch() async {
    items = await ApiService.getForkliftJob();
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width - 20;
    return Scaffold(
      body: Row(children: [
        Expanded(
          child: Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              children: <Widget>[
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: <Widget>[
                    Text(
                      'List job nhập',
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ],
                ),
                Padding(
                  padding: EdgeInsets.all(10),
                  child: Container(
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
                          controller: _listController,
                          itemBuilder: (context, index) {
                            return InkWell(
                              onTap: () {
                                setState(() {
                                  _palletController.text = items[index];
                                });
                              },
                              child: ListTile(
                                title: Text(items[index]),
                              ),
                            );
                          },
                        ),
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
        Expanded(
          child: Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              children: <Widget>[
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: <Widget>[
                    Text(
                      'Hoàn tất job nhập',
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ],
                ),
                SizedBox(
                  height: 10,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.start,
                  children: <Widget>[
                    SizedBox(
                      width: screenWidth / 2,
                      child: Container(
                        child: TextField(
                          controller: _palletController,
                          decoration: InputDecoration(
                            labelText: 'Pallet',
                          ),
                        ),
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
                          controller: _positionController,
                          decoration: InputDecoration(
                            labelText: 'Vị trí',
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
                        if (_palletController.text.isEmpty) {
                          displayDialog(context, 'Lỗi tạo pallet',
                              'Số pallet không thể để trống!');
                        }
                        if (_positionController.text.isEmpty) {
                          displayDialog(context, 'Lỗi tạo pallet',
                              'Vị trí không thể để trống!');
                        }
                        CompleteForkliftJobVm jobVm = CompleteForkliftJobVm(
                          PositionName: _positionController.text,
                          PalletNo: _palletController.text,
                        );
                        bool res = await ApiService.completeForkliftJob(jobVm);
                        if (res == true) {
                          items.remove(_palletController.text);
                          _palletController.clear();
                          _positionController.clear();
                        } else {
                          displayDialog(context, 'Lỗi hoàn tất job!', '');
                        }
                      },
                      child: Text("Hoàn tất job"),
                    )
                  ],
                ),
              ],
            ),
          ),
        ),
      ]),
    );
  }
}
