import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:flutter/material.dart';
import 'package:handheldapp/logics/ApiService.dart';
import 'package:handheldapp/models/CreatePalletDetailVm.dart';

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
  final ScrollController _listController = ScrollController();
  final int ReceiptDetailId = 0;
  List<String> items = [
/*     'Item 1',
    'Item 2',
    'Item 3',
    'Item 4',
    'Item 5',
    'Item 6',
    'Item 7',
 */
  ];
  List<String> poSuggestions = [];
  List<String> itemSuggestions = [];

  @override
  void initState() {
    super.initState();
    initDataSearch();
  }

  void initDataSearch() async {
    poSuggestions = await ApiService.availablePO();
    itemSuggestions = await ApiService.availableItem();
    setState(() {});
  }

  void displayDialog(context, title, text) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(title),
        content: Text(text),
      ),
    );
  }

  bool isScrollControllerEmpty() {
    return _listController.positions.isEmpty;
  }

  void addItemToList() {
    String po = _poController.text;
    String item = _itemController.text;
    String quantity = _quantityController.text;
    String detail = po + '-' + item + '-' + quantity;
    if (detail.isNotEmpty) {
      setState(() {
        items.add(detail);
        _poController.clear();
        _itemController.clear();
        _quantityController.clear();
        _remainController.clear();
      });
    } else {
      displayDialog(
          context, 'Lỗi tạo pallet', 'Chi tiết pallet không thể để trống!');
    }
  }

  Future<void> postPallet() async {
    List<String> allData = [];
    for (int i = 0; i < items.length; i++) {
      if (_listController.position.pixels >=
          _listController.position.maxScrollExtent) {
        // Khi cuộn xuống cuối cùng của ListView
        allData.addAll(items.sublist(i));
        break;
      }
      allData.add(items[i]);
    }
    String palletNo = _palletController.text;
    List<CreatePalletDetailVm> detailList = allData.map((data) {
      List<String> values = data.split('-');
      return CreatePalletDetailVm(
          PO: values[0],
          Item: values[1],
          PalletNo: palletNo,
          Quantity: int.parse(values[2]));
    }).toList();
    bool result = await ApiService.postDetails(detailList);
    if (!result) {
      displayDialog(
          context, 'Lỗi tạo pallet', 'Chi tiết pallet không thể để trống!');
    }
  }

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width - 20;
    return Scaffold(
      body: Column(
        children: [
          Padding(
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
                        child: SimpleAutoCompleteTextField(
                          key: GlobalKey(),
                          suggestions: poSuggestions,
                          clearOnSubmit: false,
                          controller: _poController,
                          decoration: InputDecoration(
                            labelText: 'Số PO',
                          ),
                          textSubmitted: (text) {
                            setState(() {
                              _poController.text = text;
                            });
                          },
                        ),
                      ),
                      SizedBox(
                        width: screenWidth / 2,
                        child: SimpleAutoCompleteTextField(
                          key: GlobalKey(),
                          suggestions: itemSuggestions,
                          clearOnSubmit: false,
                          controller: _itemController,
                          decoration: InputDecoration(
                            labelText: 'Số Item',
                          ),
                          textSubmitted: (text) {
                            setState(() {
                              _itemController.text = text;
                            });
                          },
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
                          int remain =
                              await ApiService.remainQuantity(po, item);
                          setState(
                            () {
                              _remainController.text = remain.toString();
                              _quantityController.text = remain.toString();
                            },
                          );
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
                          int remain =
                              int.tryParse(_remainController.text) ?? 0;
                          if (remain <= 0) {
                            displayDialog(context, 'Lỗi thêm chi tiết',
                                'Số lượng còn lại không đủ!');
                          } else {
                            addItemToList();
                          }
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
                          controller: _listController,
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
                          String palletNo = _palletController.text;
                          if (palletNo.isEmpty) {
                            displayDialog(context, 'Lỗi tạo pallet',
                                'Số pallet không thể để trống!');
                          }
                          if (isScrollControllerEmpty()) {
                            displayDialog(context, 'Lỗi tạo pallet',
                                'Danh sách PO không thể để trống!');
                          } else {
                            postPallet();
                          }
                        },
                        child: Text("Tạo pallet"),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
