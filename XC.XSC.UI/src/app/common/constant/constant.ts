export const Formconstant = {
  reviewFormAdd: './assets/review-management-form/reviewManagementFormAdd.json',
  reviewFormEdit: './assets/review-management-form/reviewManagementFormEdit.json',
  generalInformationForm: './assets/submission-general-information-form/submissionGeneralInformation.json',
  slaForm: './assets/sla/slaconfiguration.json',
  slaFormedit: './assets/sla/slaconfigurationedit.json'
}
export const submissionColumn = {  
  "columns": [
      {
         "key":"fields",
         "type":"label",
         "header":"Fields",
         "isRequred":true
      },
      {
          "key":"suggestions",
          "type":"dropdown",
          "header":"Suggetions"
      },           
      {
          "key":"finalEntry",
          "type":"textbox",
          "header":"Final Entry"
      },
      {
          "key":"confidance",
          "type":"textbox",
          "header":"Confidance %"
      }
  ]   
}

export const actions ={
  "actions":[
    {
      "id": "xceedance",
      "tenantName": "xceedance",
      "roleMappedWithAction": [
        {
          "roleId": "XXX-xxx-xxx",
          "roleName": "Processor",
          "allowedActions": [         
            "Under Query",
            "TAT Ageing",
            "Audit History"
          ]
        },
        {
          "roleId": "XXX-xxx-xxx",
          "roleName": "Reviwer",
          "allowedActions": [
            "View",
            "Play",
            "Pause",
            "Under Query",
            "TAT Ageing",
            "Audit History"
          ]
        },
        {
          "roleId": "XXX-xxx-xxx",
          "roleName": "Allocator",
          "allowedActions": [
            "Tat Ageing",
            "Under Query",
            "Re-allocation",
            "TAT Ageing",
            "Audit History"
          ]
        }
      ],
      "submissionStatusMappedAction": [
        {
          "statusId": 1,
          "statusName": "NotAssignedYet",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
  
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            }          ,
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "name": "Assign to myself",
              "css": "fa fa-street-view",
              "uri": "",
              "formName": "",
              "action_type": "assignToMyself"
            }
          ]
        },
        {
          "statusId": 2,
          "statusName": "NotStartedYet",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Send back to queue",
              "url": "../SubmissionMember",
              "className": "fa fa-arrow",
              "formName": "",
              "action_type": "SendbackToqueue"
            },
            {
              "lableName": "Play",
              "url": "",
              "className": "fa fa-play",
              "formName": "",
              "action_type": "Play"
            },
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            }
  
          ]
        },
        {
          "statusId": 3,
          "statusName": "InProgress(Play)",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Edit",
              "url": "",
              "className": "fa fa-edit",
              "formName": "",
              "action_type": "edit"
            },
            {
              "lableName": "Pause",
              "url": "",
              "className": "fa fa-pause",
              "formName": "",
              "action_type": "Pause"
            },         
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            }
          ]
        },
        {
          "statusId": 4,
          "statusName": "InProgress(Paused)",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Under Query",
              "url": "",
              "className": "fa fa-question",
              "formName": "",
              "action_type": "UnderQuery"
            },
            {
              "lableName": "Re-allocation",
              "url": "",
              "className": "",
              "formName": "",
              "action_type": ""
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            },
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            }
          ]
        },
        {
          "statusId": 5,
          "statusName": "UnderQuery",
          "allowedActions": [
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            },
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            },
            {
              "lableName": "Re-Allocation",
              "url": "",
              "className": "",
              "formName": "",
              "action_type": ""
            }
           
          ]
        },
        {
          "statusId": 6,
          "statusName": "ReviewPending",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Play",
              "url": "",
              "className": "fa fa-play",
              "formName": "",
              "action_type": "Play"
            },
            {
              "lableName": "Under Query",
              "url": "",
              "className": "fa fa-question",
              "formName": "",
              "action_type": "UnderQuery"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            },
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            }
          ]
        },
        {
          "statusId": 7,
          "statusName": "UnderReview(Play)",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Edit",
              "url": "",
              "className": "fa fa-edit",
              "formName": "",
              "action_type": "edit"
            },
            {
              "lableName": "Pause",
              "url": "",
              "className": "fa fa-pause",
              "formName": "",
              "action_type": "Pause"
            },          
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            }
           
          ]
        },
        {
          "statusId": 8,
          "statusName": "UnderReview(Paused)",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Play",
              "url": "",
              "className": "fa fa-play",
              "formName": "",
              "action_type": "Play"
            },
            {
              "lableName": "Pause",
              "url": "",
              "className": "fa fa-pause",
              "formName": "",
              "action_type": "Pause"
            },
            {
              "lableName": "Under Query",
              "url": "",
              "className": "fa fa-question",
              "formName": "",
              "action_type": "UnderQuery"
            },
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            }
            
          ]
        },
        {
          "statusId": 9,
          "statusName": "ReviewFail",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Play",
              "url": "",
              "className": "fa fa-play",
              "formName": "",
              "action_type": "Play"
            },
            {
              "lableName": "Under Query",
              "url": "",
              "className": "fa fa-question",
              "formName": "",
              "action_type": "UnderQuery"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            },
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            }
          ]
        },
        {
          "statusId": 10,
          "statusName": "ReviewPass",
          "allowedActions": [
            {
              "lableName": "View",
              "url": "../SubmissionMember",
              "className": "fa fa-eye",
              "formName": "",
              "action_type": "ViewSubmission"
            },
            {
              "lableName": "Play",
              "url": "",
              "className": "fa fa-play",
              "formName": "",
              "action_type": "Play"
            },
            {
              "lableName": "Under Query",
              "url": "",
              "className": "fa fa-question",
              "formName": "",
              "action_type": "UnderQuery"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            },
            {
              "lableName": "Reallocate",
              "url": "",
              "className": "",
              "formName": "",
              "action_type": ""
            }
          ]
        },
        {
          "statusId": 11,
          "statusName": "SubmittedToPAS",
          "allowedActions": [
            {
              "lableName": "TAT Ageing",
              "url": "",
              "className": "fa fa-hourglass",
              "formName": "",
              "action_type": "TatMetrices"
            },
            {
              "lableName": "Audit History",
              "url": "",
              "className": "fa fa-history",
              "formName": "",
              "action_type": "AuditHistory"
            }
          ]
        }
      ]
    }
  ]
}
