import 'package:flutter/material.dart';

class ExportTally extends StatefulWidget {
  @override
  State<ExportTally> createState() => _ExportTallyState();
}

class _ExportTallyState extends State<ExportTally> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Tạo job xuất'),
      ),
      body: Center(
        child: Text('Export'),
      ),
    );
  }
}
