import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:flutter/material.dart';
import 'package:handheldapp/logics/ApiService.dart';

class WrapSearch extends StatefulWidget {
  final Widget child;
  const WrapSearch({Key? key, required this.child}) : super(key: key);

  @override
  State<WrapSearch> createState() => _WrapSearchState();
}

class _WrapSearchState extends State<WrapSearch> {
  @override
  Widget build(BuildContext context) {
    return Container(child: widget.child);
  }
}

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
  List<String> suggestions = [
    "Apple",
    "Armidillo",
    "Actual",
    "Actuary",
    "America",
    "Argentina",
    "Australia",
    "Antarctica",
    "Blueberry",
    "Cheese",
    "Danish",
    "Eclair",
    "Fudge",
    "Granola",
    "Hazelnut",
    "Ice Cream",
    "Jely",
    "Kiwi Fruit",
    "Lamb",
    "Macadamia",
    "Nachos",
    "Oatmeal",
    "Palm Oil",
    "Quail",
    "Rabbit",
    "Salad",
    "T-Bone Steak",
    "Urid Dal",
    "Vanilla",
    "Waffles",
    "Yam",
    "Zest"
  ];

  SimpleAutoCompleteTextField? textField;
  GlobalKey<AutoCompleteTextFieldState<String>> key = GlobalKey();
  String currentText = "";
  late GlobalKey keySearch;

  _ImportTallyState() {
    textField = SimpleAutoCompleteTextField(
        key: key,
        decoration: InputDecoration(errorText: "Beans"),
        controller: TextEditingController(text: "Starting Text"),
        suggestions: suggestions,
        textChanged: (text) => currentText = text,
        clearOnSubmit: true,
        textSubmitted: (text) {
          print(text);
        });
  }

  @override
  void initState() {
    super.initState();
    initDataSearch();
    keySearch = GlobalKey();
  }

  void initDataSearch() async {
    suggestions = await ApiService.availablePO();
    print("--------------------------");
    print(suggestions);
    setState(() {
      textField = SimpleAutoCompleteTextField(
          key: key,
          decoration: InputDecoration(errorText: "Long Beo"),
          controller: TextEditingController(text: "aaaa"),
          suggestions: suggestions,
          textChanged: (text) => currentText = text,
          clearOnSubmit: true,
          textSubmitted: (text) {
            print(text);
          });
      keySearch = GlobalKey();
    });
  }

  void handleTextChanged(String text) async {
/*     List<String> suggestedPO = await ApiService.suggestPO(text);
    setState(() {
      suggestions = suggestedPO;
    });
 */
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

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width - 20;
    return Scaffold(
      body: Column(
        children: [
          //WrapSearch(key: keySearch, child: textField!),
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
                          suggestions: suggestions,
                          textChanged: handleTextChanged,
                          clearOnSubmit: true,
                          decoration: InputDecoration(
                            labelText: 'Số PO',
                          ),
                          textSubmitted: (text) {
                            print(text);
                          },
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
        ],
      ),
    );
  }
}
